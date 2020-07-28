using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ButtonGroundActivate : MonoBehaviour
{
    [SerializeField] private GameObject _unlockObject = null;
    [SerializeField] private bool _platformActivate = false;
    [SerializeField] private LinearMover _activatePlatform = null;
    private bool _activate = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("UnlockPart"))
        {
            this.GetComponent<Animator>().SetBool("isActive", true);
            if (_platformActivate)
            {
                if (_activate)
                {
                    _activatePlatform.ActivateMove();
                    _activate = false;
                }
                _activatePlatform.ContinueMove();
            }
            else
            {
                _unlockObject.GetComponent<Animator>().SetBool("isUnlock", true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("UnlockPart"))
        {
            this.GetComponent<Animator>().SetBool("isActive", false);
            if (_platformActivate)
            {
                _activatePlatform.StopMove();
            }
            else
            {
                _unlockObject.GetComponent<Animator>().SetBool("isUnlock", false);
            }
        }
    }

}
