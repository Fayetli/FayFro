using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoryMenu : MonoBehaviour
{
    [SerializeField] private GameObject[] _chapters;

    private void Start()
    {
        int chapterIndex = PlayerPrefs.GetInt("StoryChapter");
        for(int i = 0; i < chapterIndex + 1; i++)
        {
            _chapters[i].GetComponent<Button>().interactable = true;
            _chapters[i].GetComponent<Image>().color = new Color32(255, 255, 225, 25);
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToMenu();
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadChapter(string name)
    {
        SceneManager.LoadScene(name);
    }
}
