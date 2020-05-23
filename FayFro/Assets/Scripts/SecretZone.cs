using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SecretZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Player")
        {
            this.GetComponent<TilemapRenderer>().enabled = false;
        }
    }

}
