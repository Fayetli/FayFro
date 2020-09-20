using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Rigidbody2D>() != null)
        {
            _animator.SetFloat("VelocityX", collision.GetComponent<Rigidbody2D>().velocity.x);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Rigidbody2D>() != null)
        {
            _animator.SetFloat("VelocityX", 0.0f);
        }
    }
}
