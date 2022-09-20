using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


 
public class SceneManagement : MonoBehaviour
{
    [SerializeField] private int currentScene;
    private UIManager ui;
        private void Start()
        {
        ui = FindObjectOfType<UIManager>();
            if (Application.isPlaying)
            {
                currentScene = SceneManager.GetActiveScene().buildIndex;
                Debug.Log("You are in scene..." + currentScene);
            }
        }
    public void TryLoadScene(int sceneNumber)
    {
        if (ui.Ready2SceneChange())
        {
            SceneManager.LoadScene(sceneNumber);
        }
    }

    public void LoadRandomScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }
    public void LoadNextScene()
        {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

        public void LoadPreviousScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }

        //    //public void ICLevelLoad()
        //    //{
        //    //    SceneManager.LoadScene(SceneManager.GetSceneByName("IC Scene").ToString());
        //    //}
        //    public void ICLevelLoad()
        //    {
        //        SceneManager.LoadScene("IC Scene");
        //    }
        //    public void BoxLevelLoad()
        //    {
        //        SceneManager.LoadScene("Box Scene");
        //    }

        //    public void RetryScene()
        //    {
        //        SceneManager.LoadScene(currentScene);
        //    }

        //    public void MainMenu()
        //    {
        //        SceneManager.LoadScene(0);
        //    }

        //}
}

