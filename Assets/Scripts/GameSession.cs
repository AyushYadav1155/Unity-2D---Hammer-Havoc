using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{

    [SerializeField] int PlayerLives = 3,score=0;
    [SerializeField] Text scoreText,LivesText;
    [SerializeField] Image[] hearts;
    private void Awake()
    {
        
        int numGameSessions = FindObjectsOfType<GameSession>().Length;

        if(numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

    }

    public void ProcessPLayerDeath()
    {
        if(PlayerLives > 1)
        {
            TakeLife();
        }
        else 
        { 
            RestGame();
        }
    }

    private void RestGame()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    private void TakeLife()
    {
        PlayerLives--;
        UpdateHearts();
        LivesText.text = PlayerLives.ToString();
    }

    

    public void addToScore(int value)
    {
        score += value;
        scoreText.text = score.ToString();
    }

    public void addTolives()
    {
        PlayerLives += 1;
        if(PlayerLives >= 3)
        {
            PlayerLives = 3;
        }

        UpdateHearts();
        LivesText.text = PlayerLives.ToString();
    }

    void UpdateHearts()
    {
        for(int i=0;i<hearts.Length;i++)
        {
            if(i<PlayerLives)
            {
                hearts[i].enabled=true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }









    // Start is called before the first frame update
    void Start()
    {
        
        LivesText.text = PlayerLives.ToString();
        scoreText.text = score.ToString();


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
