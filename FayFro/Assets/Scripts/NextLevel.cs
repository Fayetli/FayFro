using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    public static void LoadNewScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}


public class NextLevel : SceneLoader
{
    [SerializeField] private string _sceneName = null;

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            LoadNewScene(_sceneName);
        }
    }

}
