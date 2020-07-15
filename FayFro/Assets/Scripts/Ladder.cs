using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private float _speed = 5;

    private float _startGravityScale;

    private Rigidbody2D rb;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<CharacterController2D>() != null)
        {
            rb = collision.gameObject.GetComponent<Rigidbody2D>();
            _startGravityScale = rb.gravityScale;
            rb.gravityScale = 0;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<CharacterController2D>() != null)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rb.velocity = new Vector2(0, _speed);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                rb.velocity = new Vector2(0, -_speed);
            }
            else
            {
                rb.velocity = new Vector2(0, 0);
            }
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<CharacterController2D>() != null)
        {
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = _startGravityScale;
            rb = null;
        }
    }

}
