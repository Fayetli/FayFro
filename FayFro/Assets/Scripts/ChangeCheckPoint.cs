using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCheckPoint : MonoBehaviour
{
    [SerializeField] private Transform CheckPoint;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Player")
        {
            DeadZoneScript.SetCheckPoint(CheckPoint);
        }
    }
}
