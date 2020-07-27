using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackElement : MonoBehaviour
{



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<CharacterController2D>() != null)
        {
            Debug.Log("Take damage");
        }
        else if(collision.gameObject.GetComponent<Box>() != null)
        {
            collision.gameObject.GetComponent<ResetObject>().Destroy();
        }
    }
}
