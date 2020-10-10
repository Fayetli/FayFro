using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnlockPart : MonoBehaviour
{
    private UnlockController _controller;
    public UnityEvent OnUnlock;
    private bool _PlayerStay = false;
    void Start()
    {
        _controller = GameObject.FindObjectOfType<UnlockController>();

        if (OnUnlock == null)
            OnUnlock = new UnityEvent();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<CharacterController2D>() != null)
        {
            _PlayerStay = true;
        }
    }

    private void Update()
    {
        if (_PlayerStay)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Use();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<CharacterController2D>() != null)
        {
            _PlayerStay = false;
        }
    }

    private void Use()
    {
        _controller.AddCurrentUnlockPartCount();
        this.GetComponent<Animator>().SetBool("isUnlock", true);
        OnUnlock.Invoke();
        Destroy(this);
    }
}
