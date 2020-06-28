using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockController : MonoBehaviour
{
    
    int CurrentUnlockPartCount;
    int AllUnlockPartCount;

    void Start()
    {
        GameObject[] unlockParts = GameObject.FindGameObjectsWithTag("UnlockPart");
        AllUnlockPartCount = unlockParts.Length;
        CurrentUnlockPartCount = 0;
    }

    public void AddCurrentUnlockPartCount()
    {
        if(CurrentUnlockPartCount == AllUnlockPartCount - 1)
        {
            this.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
            this.GetComponent<Animator>().SetBool("isUnlock", true);
        }
        else
        {
            CurrentUnlockPartCount += 1;
        }
    }
}
