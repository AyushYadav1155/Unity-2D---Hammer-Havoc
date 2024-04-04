using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondPickup : MonoBehaviour
{
    [SerializeField] AudioClip DiamondPickSFX;
    [SerializeField] int diamondScore = 100;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(DiamondPickSFX,Camera.main.transform.position);
        FindObjectOfType<GameSession>().addToScore(diamondScore);
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
