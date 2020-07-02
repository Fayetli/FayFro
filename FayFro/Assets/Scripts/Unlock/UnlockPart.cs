using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockPart : MonoBehaviour
{
    private UnlockController _controller;
    private bool IsUnlock;
    void Start()
    {
        _controller = GameObject.FindGameObjectWithTag("UnlockController").gameObject.GetComponent<UnlockController>();
        IsUnlock = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (!IsUnlock)
                {
                    IsUnlock = true;
                    _controller.AddCurrentUnlockPartCount();
                    this.GetComponent<Animator>().SetBool("isUnlock", true);
                }
            }
        }
    }
}
