using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 15f;
    [SerializeField] float climbingSpeed = 8f;
    [SerializeField] Vector2 hitKick = new Vector2(50f, 50f);
    [SerializeField] Transform hurtBox;
    [SerializeField] float attackRadius = 3f;
    [SerializeField] AudioClip jumpingSFX, attackingSFX, playerHitSFX, walkingSFX;

    Rigidbody2D myRigidBody2D;

    Animator myAnimation;

    BoxCollider2D myCollider;

    PolygonCollider2D myPolygonCollider;

    AudioSource myAudioSource;

    float startingGravScale;
    bool isHurting = false;
    // Start is called before the first frame update
    void Start()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>();
        myAnimation = GetComponent<Animator>();
        myCollider = GetComponent<BoxCollider2D>();
        myPolygonCollider = GetComponent<PolygonCollider2D>();
        startingGravScale = myRigidBody2D.gravityScale;
        myAudioSource = GetComponent<AudioSource>();

        myAnimation.SetTrigger("Appearing");
    }

    // Update is called once per frame
    void Update()
    {

        if (!isHurting)
        {
            run();
            jump();
            Climb();
            Attack();

            if (myCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")))
            {
                PlayerHit();
            }

            ExitLevel();

        }


    }

    private void ExitLevel()
    {
        if (!myCollider.IsTouchingLayers(LayerMask.GetMask("Interactable")))
        {
            return;
        }
        if (CrossPlatformInputManager.GetButtonDown("Vertical"))
        {
            myAnimation.SetTrigger("disappering");

        };
    }

    public void TurnOffRenderer()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }

    public void LoadnextLevel()
    {

        FindObjectOfType<ExitDoor>().StartLoadingNextLevel();
        TurnOffRenderer();
    }

    private void Attack()
    {
        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            myAnimation.SetTrigger("Attacking");
            myAudioSource.PlayOneShot(attackingSFX);

            Collider2D[] enemiesToHit = Physics2D.OverlapCircleAll(hurtBox.position, attackRadius, LayerMask.GetMask("Enemy"));

            foreach (Collider2D enemy in enemiesToHit)
            {

                enemy.GetComponent<Enemy>().Dying();


            }
        }
    }



    public void PlayerHit()
    {
        myRigidBody2D.velocity = hitKick * new Vector2(-transform.localScale.x, 1f);

        myAnimation.SetTrigger("Hitting");

        isHurting = true;

        FindObjectOfType<GameSession>().ProcessPLayerDeath();
        myAudioSource.PlayOneShot(playerHitSFX);

        StartCoroutine(StopHurting());
    }

    IEnumerator StopHurting()
    {
        yield return new WaitForSeconds(2f);
        isHurting = false;
    }

    public void Climb()
    {
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
            Vector2 climbingVelo = new Vector2(myRigidBody2D.velocity.x, controlThrow * climbingSpeed);

            myRigidBody2D.velocity = climbingVelo;

            myAnimation.SetBool("climbing", true);
            myRigidBody2D.gravityScale = 0f;

        }
        else
        {
            myAnimation.SetBool("climbing", false);
            myRigidBody2D.gravityScale = startingGravScale;
        }
    }

    private void jump()
    {
        if (!myPolygonCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) {

            return;

        }
        bool isJummping = CrossPlatformInputManager.GetButtonDown("Jump");
        if (isJummping)
        {
            Vector2 jumpVel = new Vector2(myRigidBody2D.velocity.x, jumpSpeed);
            myRigidBody2D.velocity = jumpVel;
            myAudioSource.PlayOneShot(jumpingSFX);
        }
    }

    private void run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");

        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody2D.velocity.y);
        myRigidBody2D.velocity = playerVelocity;
        flipSprite();
        ChangingToRunning();
    }

    public void StepsSFX()
    {
        bool playerMvoingHori = Mathf.Abs(myRigidBody2D.velocity.x) > Mathf.Epsilon;

        if (playerMvoingHori)
        {
            if (myCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                myAudioSource.PlayOneShot(walkingSFX);
            }
        }
        else
        {
            myAudioSource.Stop();
        }
    } 

    private void ChangingToRunning()
    {
        bool RunHori = Mathf.Abs(myRigidBody2D.velocity.x) > Mathf.Epsilon;
        myAnimation.SetBool("running", RunHori);
    }

    private void flipSprite()
    {
        bool RunHori = Mathf.Abs(myRigidBody2D.velocity.x) > Mathf.Epsilon;

        if (RunHori)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody2D.velocity.x), 1f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(hurtBox.position, attackRadius);
    }
}
