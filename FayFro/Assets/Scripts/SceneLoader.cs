using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : DefaultSceneLoader
{
    [SerializeField] private string _sceneName = null;

    [SerializeField] private Mode _mode;

    enum Mode
    {
        Index,
        Name
    }



    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<CharacterController2D>() != null)
        {
            switch (_mode)
            {
                case Mode.Index:
                    {
                        LoadNextScene();
                        break;
                    }
                case Mode.Name:
                    {
                        LoadNextScene(_sceneName);
                        break;
                    }
            }
            
        }
    }

    public void LoadNextScene(string name = "")
    {
        if(name == "")
        {
            StartCoroutine(LoadNewScene());
        }
        else
        {
            StartCoroutine(LoadNewScene(name));
        }
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }
}
