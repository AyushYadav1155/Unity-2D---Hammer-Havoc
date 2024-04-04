using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour

    
{
    [SerializeField] AudioClip closeDoorSFX;

    [SerializeField] float SecsToLoad = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<Animator>().SetTrigger("Open");
    }

    public void StartLoadingNextLevel()
    {
        GetComponent<Animator>().SetTrigger("Close");

        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(SecsToLoad);

        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void PlayClosingDoorSFX()
    {

        AudioSource.PlayClipAtPoint(closeDoorSFX,Camera.main.transform.position);

    }
}
