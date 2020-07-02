using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour
{

    [SerializeField] private GameObject _unlockObject;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (collision.name == "Player")
            {
                bool leverActive = this.GetComponent<Animator>().GetBool("isActive") ? false : true;
                this.GetComponent<Animator>().SetBool("isActive", leverActive);

                LinearMover objectMover = _unlockObject.GetComponent<LinearMover>();
                objectMover.MoveObject();
                
            }
        }
    }
}
