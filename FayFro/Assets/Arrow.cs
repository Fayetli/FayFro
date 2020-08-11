using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float _addForce = 750f;
    [SerializeField] private bool _IsRight = true;

    private int _direction; //-1 - left, 1 - right
    private void Start()
    {
        _direction = -1;
        if(_IsRight == true)
        {
            _direction = 1;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<CharacterController2D>() != null)
        {
            Rigidbody2D rigidbody = collision.gameObject.GetComponent<Rigidbody2D>();

            rigidbody.AddForce(new Vector2(_addForce * _direction * rigidbody.mass, 0f));

            rigidbody.velocity = new Vector3(0, 0, 0);
        }
    }
}
