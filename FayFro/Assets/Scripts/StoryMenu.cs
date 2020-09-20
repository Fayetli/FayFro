using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class StoryMenu : MonoBehaviour
{
    [SerializeField] private GameObject[] _chapters;
    [SerializeField] private TextMeshProUGUI[] _chaptersScore;
    [SerializeField] private GameObject[] _scorePanels;

    private void Start()
    {
        int chapterIndex = PlayerPrefs.GetInt("StoryChapter");
        for(int i = 0; i < chapterIndex + 1; i++)
        {
            _chapters[i].GetComponent<Button>().interactable = true;
            _chapters[i].GetComponent<Image>().color = new Color32(255, 255, 225, 25);
        }

        for(int i = 0; i < chapterIndex; i++)
        {
            BonusController.GetActivatedInfoForStage(out int activatedCount, out int allCount, i);

            _chaptersScore[i].text = activatedCount.ToString() + "/" + allCount.ToString();
        }

        for (int i = chapterIndex; i < _scorePanels.Length; i++)
        {
            _scorePanels[i].SetActive(false);
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
