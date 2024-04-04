using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] float enemyRunSpeed = 5f;
    [SerializeField] AudioClip enemyDying;

    Rigidbody2D enemyRigidBody;
    Animator myAnimator;
    // Start is called before the first frame update
    void Start()
    {
        enemyRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovement();
        

    }

    public void Dying()
    {
        myAnimator.SetTrigger("Die");
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        enemyRigidBody.bodyType = RigidbodyType2D.Static;

        StartCoroutine(DestoryEnemy());
    }

    IEnumerator DestoryEnemy()
    {
        yield return new WaitForSeconds(2f) ;
        Destroy(gameObject);
    }

    private void EnemyMovement()
    {
        if (isFacingLeft())
        {
            enemyRigidBody.velocity = new Vector2(-enemyRunSpeed, 0f);
        }
        else
        {
            enemyRigidBody.velocity = new Vector2(enemyRunSpeed, 0f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        FlipSprite();
    }

    private void FlipSprite()
    {
        transform.localScale = new Vector2(Mathf.Sign(enemyRigidBody.velocity.x),1f);
    }

    private bool isFacingLeft()
    {
        return transform.localScale.x > 0;
    }

    public void DyingEnemySFX()
    {
        AudioSource.PlayClipAtPoint(enemyDying,Camera.main.transform.position);
    }
}
