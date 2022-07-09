using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    [SerializeField] private int currentScene;


    private void Start()
    {
        if (Application.isPlaying)
        {
            currentScene = SceneManager.GetActiveScene().buildIndex;
            Debug.Log("You are in scene..." + currentScene);
        }
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadPreviousScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    //public void ICLevelLoad()
    //{
    //    SceneManager.LoadScene(SceneManager.GetSceneByName("IC Scene").ToString());
    //}
    public void ICLevelLoad()
    {
        SceneManager.LoadScene("IC Scene");
    }
    public void BoxLevelLoad()
    {
        SceneManager.LoadScene("Box Scene");
    }

    public void RetryScene()
    {
        SceneManager.LoadScene(currentScene);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

}
