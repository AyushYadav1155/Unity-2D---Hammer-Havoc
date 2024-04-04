using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] float radius=3f;
    [SerializeField] Vector2 explosionF = new Vector2(200f, 100f);

    [SerializeField]AudioClip buringSFX, explodingSFX;

    Animator myAnimator;
    AudioSource myAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ExplodBomb()
    {
        Collider2D playerColider =  Physics2D.OverlapCircle(transform.position, radius, LayerMask.GetMask("Player"));
        myAudioSource.PlayOneShot(explodingSFX);
        if (playerColider )
        {
            playerColider.GetComponent<Rigidbody2D>().AddForce(explosionF);

            playerColider.GetComponent<Player>().PlayerHit();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        myAnimator.SetTrigger("Bomb on");
        myAudioSource.PlayOneShot(buringSFX);
    }

    void DestroyBomb()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
