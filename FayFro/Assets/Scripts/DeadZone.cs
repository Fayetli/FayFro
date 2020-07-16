using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DeadZone : MonoBehaviour
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


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<CharacterController2D>() != null)
        {
            collision.transform.position = _checkPoint.position;
        }
    }
}
