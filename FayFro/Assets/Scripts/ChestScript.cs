using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (anim.GetBool("isOpened") != true)
        {
            if (obj.transform.name == "Player" && Input.GetKeyDown(KeyCode.Z))
            {
                anim.SetBool("isOpened", true);
                StatCharacterController.player.AddScore(addPoint);
                InterfaceManager.PrintScore();
            }
        }
    }
}
