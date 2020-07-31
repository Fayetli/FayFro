using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ButtonGroundActivate : MonoBehaviour
{
    [SerializeField] private GameObject _activateObject = null;
    [SerializeField] private Mode mode;
    private bool _activate = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("UnlockPart"))
        {
            this.GetComponent<Animator>().SetBool("isActive", true);
            switch (mode)
            {
                case Mode.AnimationObject:
                    {
                        _activateObject.GetComponent<Animator>().SetBool("isUnlock", true);
                        break;
                    }
                case Mode.LinearPlatform:
                    {
                        if (_activate)
                        {
                            _activateObject.GetComponent<LinearMover>().ActivateMove();
                            _activate = false;
                        }
                        _activateObject.GetComponent<LinearMover>().ContinueMove();
                        break;
                    }
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("UnlockPart"))
        {
            this.GetComponent<Animator>().SetBool("isActive", false);
            switch (mode)
            {
                case Mode.AnimationObject:
                    {
                        _activateObject.GetComponent<Animator>().SetBool("isUnlock", false);
                        break;
                    }
                case Mode.LinearPlatform:
                    {
                        _activateObject.GetComponent<LinearMover>().StopMove();
                        break;
                    }
            }
        }
    }

}
