using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZoneScript : MonoBehaviour
{
    static private Transform _checkPoint;

    public static void SetCheckPoint(Transform transform)//when triggered
    {
        _checkPoint = transform;
    }
    private void Start()
    {
        _checkPoint = GameObject.Find("CheckPoint(0)").gameObject.transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if(collision.transform.tag == "Player")
        {
            if (StatCharacterController.player.IsOneHP())
            {
                StatCharacterController.player.SetHealthToMaxHealth();
                collision.transform.position = _checkPoint.position;
            }
            else
            {
                StatCharacterController.player.Health++; ;
            }
            InterfaceManager.PrintHealth();
        }
    }
}
