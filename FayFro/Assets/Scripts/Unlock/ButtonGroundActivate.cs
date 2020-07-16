using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ButtonGroundActivate : MonoBehaviour
{
    [SerializeField] private GameObject _unlockObject = null;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("UnlockPart"))
        {
            this.GetComponent<Animator>().SetBool("isActive", true);
            //_unlockObject.GetComponent<LinearMover>().
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("UnlockPart"))
        {
            this.GetComponent<Animator>().SetBool("isActive", false);
            _unlockObject.GetComponent<Animator>().SetBool("isUnlock", false);
        }
    }

}
