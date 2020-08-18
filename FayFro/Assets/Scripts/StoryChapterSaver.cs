using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryChapterSaver : MonoBehaviour
{
    [SerializeField] private int _chapterIndex;
    private void Start()
    {
        if(_chapterIndex > PlayerPrefs.GetInt("StoryChapter"))
        {
            PlayerPrefs.SetInt("StoryChapter", _chapterIndex);
        }
    }

}
