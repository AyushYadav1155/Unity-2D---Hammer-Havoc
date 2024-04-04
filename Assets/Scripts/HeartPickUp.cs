using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPickUp : MonoBehaviour
{
    [SerializeField] AudioClip HeartPickSFX;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(HeartPickSFX, Camera.main.transform.position);
        FindObjectOfType<GameSession>().addTolives();
        Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
