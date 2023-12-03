using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] Scene sNextScene;

    int intNextSceneIndex = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Before " + intNextSceneIndex);
            intNextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            Debug.Log("After " + intNextSceneIndex);

            Debug.Log("SceneManager.sceneCount " + SceneManager.sceneCountInBuildSettings);
            if (intNextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(intNextSceneIndex);
            }

            else
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
