using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StorySaver : MonoBehaviour
{
    [SerializeField] private bool _OnLvl = true;
    private void Awake()
    {
        if (_OnLvl)
        {
            PlayerPrefs.SetInt("LastSceneIndex", SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void LoadLastLevel()
    {
        int lastSceneIndex = PlayerPrefs.GetInt("LastSceneIndex");
        Debug.Log(lastSceneIndex);
        SceneManager.LoadScene(lastSceneIndex);
    }
}
