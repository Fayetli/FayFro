using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnlockController : MonoBehaviour
{

    private int _currentUnlockPartCount;
    private int _allUnlockPartCount = 0;

    public UnityEvent OnUnlock;

    void Start()
    {
        _allUnlockPartCount = GameObject.FindObjectsOfType<UnlockPart>().Length;

        if(OnUnlock == null)
        {
            OnUnlock = new UnityEvent();
        }
    }

    public void AddCurrentUnlockPartCount()
    {
        _currentUnlockPartCount += 1;

        if (_currentUnlockPartCount == _allUnlockPartCount)
        {
            this.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
            this.GetComponent<Animator>().SetBool("isUnlock", true);
            OnUnlock.Invoke();
        }
    }
}
