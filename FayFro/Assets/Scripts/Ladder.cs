using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    [SerializeField] private float speed = 5;



    float startGravityScale;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            startGravityScale = collision.gameObject.GetComponent<Rigidbody2D>().gravityScale;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.gravityScale = 0;

            if (Input.GetKey(KeyCode.UpArrow))
            {
                rb.velocity = new Vector2(0, speed);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                rb.velocity = new Vector2(0, -speed);
            }
            else
            {
                rb.velocity = new Vector2(0, 0);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = startGravityScale;
        }
    }

}
