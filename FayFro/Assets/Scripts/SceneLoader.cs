using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DefaultSceneLoader : MonoBehaviour
{
    public static IEnumerator LoadNewScene(string sceneName)
    {
        float fadeTime = GameObject.FindObjectOfType<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(sceneName);
    }

    public static IEnumerator LoadNewScene()
    {
        float fadeTime = GameObject.FindObjectOfType<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}


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
                        StartCoroutine(LoadNewScene());
                        break;
                    }
                case Mode.Name:
                    {
                        StartCoroutine(LoadNewScene(_sceneName));
                        break;
                    }
            }
            
        }
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }
}
