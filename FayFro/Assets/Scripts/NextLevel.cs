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

    [SerializeField] private bool _haveResetObjects = true;

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            if (_haveResetObjects)
            {
                ResetObject[] resetObjects = GameObject.FindObjectsOfType<ResetObject>();
                foreach(ResetObject resetObject in resetObjects)
                {
                    resetObject._reset = false;
                }
            }
            LoadNewScene(_sceneName);
        }
    }

}
