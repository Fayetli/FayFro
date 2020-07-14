using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverActivate : MonoBehaviour
{
    [SerializeField] private GameObject _unlockObject = null;



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (collision.name == "Player")
            {
                if (this.GetComponent<Animator>().GetBool("isActive"))
                {
                    this.GetComponent<Animator>().SetBool("isActive", false);
                    _unlockObject.GetComponent<Animator>().SetBool("isUnlock", false);
                }
                else
                {
                    this.GetComponent<Animator>().SetBool("isActive", true);
                    _unlockObject.GetComponent<Animator>().SetBool("isUnlock", true);
                }
            }
        }
    }

}
