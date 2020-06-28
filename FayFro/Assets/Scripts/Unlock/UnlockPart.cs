using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockPart : MonoBehaviour
{
    UnlockController controller;
    bool isUnlock;
    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("UnlockController").gameObject.GetComponent<UnlockController>();
        isUnlock = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (!isUnlock)
                {
                    isUnlock = true;
                    controller.AddCurrentUnlockPartCount();
                    this.GetComponent<Animator>().SetBool("isUnlock", true);
                }
            }
        }
    }
}
