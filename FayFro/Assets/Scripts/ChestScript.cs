using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// DONT USING
/// </summary>



public class ChestScript : MonoBehaviour
{
    public int addPoint;
    private Animator anim;

    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    private void OnTriggerStay2D(Collider2D obj)
    {
        if (anim.GetBool("isUnlock") != true)
        {
            if (obj.transform.name == "Player" && Input.GetKeyDown(KeyCode.Z))
            {
                anim.SetBool("isUnlock", true);
                StatCharacterController.player.Score += addPoint;
                InterfaceManager.PrintScore();
            }
        }
    }
}
