using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherCollector : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Player")
        {
            StatCharacterController.player.AddScore(10);
            InterfaceManager.PrintScore();
            //mb anim
            Destroy(this.gameObject);
        }
    }



}
