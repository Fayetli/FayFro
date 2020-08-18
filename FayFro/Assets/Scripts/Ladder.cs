using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private float _speed = 3;

    private float _startGravityScale;

    private Rigidbody2D rb;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<CharacterController2D>() != null)
        {
            collision.gameObject.GetComponent<Animator>().SetBool("onLadder", true);
            rb = collision.gameObject.GetComponent<Rigidbody2D>();
            _startGravityScale = rb.gravityScale;
            rb.gravityScale = 0;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<CharacterController2D>() != null)
        {
            if (collision.gameObject.GetComponent<BoxPlayerMover>().IsBox == false)
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
                collision.gameObject.GetComponent<Animator>().SetFloat("vSpeed", rb.velocity.y);
            }
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<CharacterController2D>() != null)
        {
            collision.gameObject.GetComponent<Animator>().SetBool("onLadder", false);
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = _startGravityScale;
            rb = null;
        }
    }

}
