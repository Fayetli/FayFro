using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockController : MonoBehaviour
{
    
    private int _currentUnlockPartCount;
    private int _allUnlockPartCount;

    void Start()
    {
        GameObject[] unlockParts = GameObject.FindGameObjectsWithTag("UnlockPart");
        _allUnlockPartCount = unlockParts.Length;
        _currentUnlockPartCount = 0;
    }

    public void AddCurrentUnlockPartCount()
    {
        if(_currentUnlockPartCount == _allUnlockPartCount - 1)
        {
            this.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
            this.GetComponent<Animator>().SetBool("isUnlock", true);
        }
        else
        {
            _currentUnlockPartCount += 1;
        }
    }
}
