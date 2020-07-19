using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPlatform : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Box>() != null)
        {
            Debug.Log("Boss attacked!");
        }
    }
}
