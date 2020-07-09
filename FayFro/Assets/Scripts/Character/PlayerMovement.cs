using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController2D))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController2D _controller;

    [SerializeField] private bool _HaveTP = false;

    [SerializeField] private bool _HaveShield = false;

    private float _horizontalMove = 0f;

    private float _runSpeed = 7.5f;

    private bool _jump = false;

    private bool _crouch;

    private bool _dash = false;

    private bool _useShield = false;

    private void Start()
    {
        _controller = GetComponent<CharacterController2D>();
    }
void Update()
    {

        _horizontalMove = Input.GetAxisRaw("Horizontal") * _runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            _jump = true;
        }

        if (Input.GetButtonDown("Crouch"))
        {
            _crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            _crouch = false;
        }

        if (Input.GetKeyDown(KeyCode.X) && _HaveTP)
        {
            _dash = true;
        }
        if (Input.GetKeyDown(KeyCode.V) && _HaveShield)
        {
            _useShield = true;
        }
        else if (Input.GetKeyUp(KeyCode.V) && _HaveShield)
        {
            _useShield = false;
        }

    }


    private void FixedUpdate()
    {
        //Move our character
        _controller.Move(_horizontalMove * Time.fixedDeltaTime, _crouch, _jump, _dash, _useShield);

        _jump = false;

        _dash = false;
    }
}
