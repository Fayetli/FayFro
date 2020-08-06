using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Mode
{
    AnimationObject,
    LinearPlatform,
    InfinityPlatform
}


[RequireComponent(typeof(BoxCollider2D))]
public class LeverActivate : MonoBehaviour
{
    [SerializeField] private GameObject _activateObject = null;
    [SerializeField] private Mode mode;
    [SerializeField] private bool _activate = true;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (collision.GetComponent<CharacterController2D>() != null)
            {
                if (gameObject.GetComponent<Animator>().GetBool("isActive"))
                {
                    gameObject.GetComponent<Animator>().SetBool("isActive", false);
                    switch (mode)
                    {
                        case Mode.AnimationObject:
                            {
                                _activateObject.GetComponent<Animator>().SetBool("isUnlock", false);
                                break;
                            }
                        case Mode.InfinityPlatform:
                            {
                                InfinityLinearMover mover = _activateObject.GetComponent<InfinityLinearMover>();
                                mover.StopAllCoroutines();
                                mover.ChangeVector();
                                mover.StartMove();
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
                else
                {
                    gameObject.GetComponent<Animator>().SetBool("isActive", true);
                    switch (mode)
                    {
                        case Mode.AnimationObject:
                            {
                                _activateObject.GetComponent<Animator>().SetBool("isUnlock", true);
                                break;
                            }
                        case Mode.InfinityPlatform:
                            {
                                InfinityLinearMover mover = _activateObject.GetComponent<InfinityLinearMover>();
                                mover.StopAllCoroutines();
                                mover.ChangeVector();
                                mover.StartMove();
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
    }

}
