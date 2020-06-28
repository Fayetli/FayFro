using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelScript : MonoBehaviour
{
    public string sceneName;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            SceneManager.LoadScene(sceneName);
        }
    }

}
