using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private GameObject _player;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private float _startGravityScale;

    private bool _Ladding = false;
    private bool _NoExit = false;
    private float _speed = 3;

    private void Start()
    {
        _player = GameObject.FindObjectOfType<CharacterController2D>().gameObject;
        _animator = _player.GetComponent<Animator>();
        _rigidbody = _player.GetComponent<Rigidbody2D>();
        _startGravityScale = _rigidbody.gravityScale;
    }


    private void FixedUpdate()
    {
        Debug.Log(_NoExit);
        if (_Ladding)
        {
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
            {
                if (_NoExit == false)
                {
                    float direction = Input.GetAxisRaw("Horizontal");

                    _player.transform.position = new Vector2(_player.transform.position.x + 0.5f * direction, _player.transform.position.y);

                    ExitFromLadder();
                }
                else
                {
                    _animator.SetFloat("vSpeed", _rigidbody.velocity.y);
                }
                return;
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                _rigidbody.velocity = new Vector2(0, _speed);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                _rigidbody.velocity = new Vector2(0, -_speed);
            }
            else
            {
                _rigidbody.velocity = new Vector2(0, 0);
            }

            _animator.SetFloat("vSpeed", _rigidbody.velocity.y);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            if (_Ladding == false && collision.gameObject == _player)
            {
                EnterToLadder();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<CharacterController2D>() != null)
        {
            if (_Ladding)
            {
                ExitFromLadder();
            }
        }
    }

    private void CanExitAfter()
    {
        _NoExit = false;
    }

    private void ExitFromLadder()
    {
        CancelInvoke("CanExitAfter");

        _animator.SetBool("onLadder", false);

        _rigidbody.gravityScale = _startGravityScale;
        _player.GetComponent<PlayerMovement>().ChangeLadding(false);

        _Ladding = false;
        _NoExit = true;
    }

    private void EnterToLadder()
    {
        CancelInvoke("CanExitAfter");

        _animator.SetBool("onLadder", true);

        _player.transform.position = new Vector2(gameObject.transform.position.x, _player.transform.position.y);
        _player.GetComponent<PlayerMovement>().ChangeLadding(true);
        _rigidbody.gravityScale = 0;

        _Ladding = true;
        _NoExit = true;

        Invoke("CanExitAfter", 0.5f);

    }
}
