using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


[System.Serializable]
public class Bonus
{
    public int sceneID;
    public bool isActivated;
}

[System.Serializable]
public class Bonuses
{
    public List<ChapterBonuses> chapters;

    [System.Serializable]
    public class ChapterBonuses
    {
        public List<Bonus> bonuses;

        public Bonus this[int index]
        {
            get
            {
                return bonuses[index];
            }

            set
            {
                bonuses[index] = value;
            }
        }
    }

    public ChapterBonuses this[int index]
    {
        get
        {
            return chapters[index];
        }

        set
        {
            chapters[index] = value;
        }
    }
}

public class BonusController : MonoBehaviour
{
    public static Bonuses _all;

    const string fileName = "Bonuses.json";
    private void Awake()
    {
        Debug.Log(GetPath());
        Load();
        DebugOutPut();
    }

    public void Load()
    {

        string json;

        string path = GetPath();

        if(File.Exists(path) == false)
        {
            TextAsset textAssetBonuses = Resources.Load("PreloadData/Bonuses") as TextAsset;

            json = textAssetBonuses.text;

        }
        else
        {
            FileStream fileStream = new FileStream(GetPath(), FileMode.Open);

            StreamReader reader = new StreamReader(fileStream);

            json = reader.ReadToEnd();

        }

        _all = JsonUtility.FromJson<Bonuses>(json);

    }

    private static void Upload()
    {
        string path = GetPath();
        if (File.Exists(path))
            File.Delete(path);
        

        FileStream fileStream = new FileStream(GetPath(), FileMode.Create);

        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            string json = JsonUtility.ToJson(_all, true);

            writer.Write(json);
        }

    }

    private static string GetPath()
    {
        return Application.persistentDataPath + "/" + fileName;
    }

    public static void ResetAllProgress()
    {
        foreach (Bonuses.ChapterBonuses chapter in _all.chapters)
        {
            foreach(Bonus bonus in chapter.bonuses)
            {
                bonus.isActivated = false;
            }
        }
        Upload();
    }

    public static void DebugOutPut()
    {
        foreach (Bonuses.ChapterBonuses chapter in _all.chapters)
        {
            foreach (Bonus bonus in chapter.bonuses)
            {
                Debug.Log("Scene: " + bonus.sceneID + ", Activated: " + bonus.isActivated);
            }
        }
    }

    public static bool CheckForActivated(int stageID, int sceneID)
    {
        Bonuses.ChapterBonuses chapter = _all[stageID];

        foreach(Bonus bonus in chapter.bonuses)
        {
            if(bonus.sceneID == sceneID)
            {
                return bonus.isActivated;
            }
        }
        return false;
    }

    public static void SetToActivated(int stageID, int sceneID)
    {
        Bonuses.ChapterBonuses chapter = _all[stageID];

        foreach (Bonus bonus in chapter.bonuses)
        {
            if (bonus.sceneID == sceneID)
            {
                bonus.isActivated = true;
            }
        }
        Upload();
    }

    public static void GetActivatedInfoForStage(out int activatedCount, out int allCount, int stageID)
    {
        Bonuses.ChapterBonuses chapter = _all[stageID];

        allCount = chapter.bonuses.Count;

        activatedCount = 0;

        foreach (Bonus bonus in chapter.bonuses)
        {
            if (bonus.isActivated)
            {
                activatedCount++;
            }

        }
    }
}
