using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnlockPart : MonoBehaviour
{
    private UnlockController _controller;
    public UnityEvent OnUnlock;
    void Start()
    {
        _controller = GameObject.FindObjectOfType<UnlockController>();

        if (OnUnlock == null)
            OnUnlock = new UnityEvent();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<CharacterController2D>() != null)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                _controller.AddCurrentUnlockPartCount();
                this.GetComponent<Animator>().SetBool("isUnlock", true);
                OnUnlock.Invoke();
                Destroy(this);
            }
        }
    }
}
