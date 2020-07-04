using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;

    private float _horizontalMove = 0f;

    private float _runSpeed = 7.5f;

    private bool _jump = false;

    private bool _crouch;
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

    }


    private void FixedUpdate()
    {
        //Move our character
        controller.Move(_horizontalMove * Time.fixedDeltaTime, _crouch, _jump);

        _jump = false;


    }
}
