using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelScript : MonoBehaviour
{
    public string sceneName;

    public void LoadNewScene(string scene_name)
    {
        SceneManager.LoadScene(scene_name);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            LoadNewScene(sceneName);
        }
    }

}
