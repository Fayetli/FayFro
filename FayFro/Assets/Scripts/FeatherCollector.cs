using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherCollector : MonoBehaviour
{
    [SerializeField] private int _addScore = 10;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Player")
        {
            Score._value += _addScore;
            //mb anim
            Destroy(this.gameObject);
        }
    }
}
