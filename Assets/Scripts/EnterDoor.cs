using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterDoor : MonoBehaviour
{
    [SerializeField] AudioClip openDoorSFX;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().SetTrigger("Open");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayOpeningDoorSFX()
    {
        AudioSource.PlayClipAtPoint(openDoorSFX, Camera.main.transform.position);
    }

    
}
