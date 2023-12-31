using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int intPlayerLives = 3;

    private void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }

        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ProcessPlayerDeath()
    {
        if (intPlayerLives > 1)
        {
            TakeLife();
        }

        else
        {
            ResetGameSession();
        }
    }

    private void TakeLife()
    {
        intPlayerLives--;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
