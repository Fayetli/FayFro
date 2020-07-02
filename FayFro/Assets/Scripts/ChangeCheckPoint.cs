using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCheckPoint : MonoBehaviour
{
    [SerializeField] private Transform _checkPoint;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Player")
        {
            DeadZoneScript.SetCheckPoint(_checkPoint);
        }
    }
}
