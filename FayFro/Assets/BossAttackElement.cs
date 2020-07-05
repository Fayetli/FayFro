using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackElement : MonoBehaviour
{



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            Debug.Log("Take damage");
        }
    }
}
