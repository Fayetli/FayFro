using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZoneScript : MonoBehaviour
{
    static private Transform checkPoint;

    public void SetCheckPoint(Transform tr)//when triggered
    {
        checkPoint = tr;
    }
    private void Start()
    {
        checkPoint = GameObject.Find("CheckPoint(0)").gameObject.transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if(collision.transform.tag == "Player")
        {
            if (StatCharacterController.player.IsOneHP())
            {
                StatCharacterController.player.SetHealthToMaxHealth();
                collision.transform.position = checkPoint.position;
            }
            else
            {
                StatCharacterController.player.AddHealth(-1);
            }
            InterfaceManager.PrintHealth();
        }
    }
}
