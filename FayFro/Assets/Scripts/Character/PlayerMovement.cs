﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController2D), typeof(BoxPlayerMover))]
public class PlayerMovement : MonoBehaviour, PlatformerObject
{
    private CharacterController2D _controller;
    private BoxPlayerMover _boxMover;

    [SerializeField] private bool _HaveTP = false;

    [SerializeField] private bool _HaveShield = false;

    private float _horizontalMove = 0f;

    private float _runSpeed = 33f;

    private bool _jump = false;

    private bool _crouch;

    private bool _dash = false;

    private bool _useShield = false;

    private bool _isNotToFlip = false;

    private void Start()
    {
        _controller = GetComponent<CharacterController2D>();
        _boxMover = GetComponent<BoxPlayerMover>();
    }
    void FixedUpdate()
    {

        _horizontalMove = Input.GetAxis("Horizontal");
        
        _horizontalMove *= _runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            _jump = true;
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

        if (Input.GetKey(KeyCode.Z))
        {
            bool isBox = _boxMover.IsBox();
            if (isBox)
            {
                _isNotToFlip = true;
                _horizontalMove /= 2;
            }

        }
        
        _controller.Move(_horizontalMove * Time.fixedDeltaTime, _jump, _dash, _useShield, _isNotToFlip);

        _jump = false;

        _dash = false;

        _isNotToFlip = false;
    }


    //private void FixedUpdate()
    //{
    //    _controller.Move(_horizontalMove * Time.fixedDeltaTime, _jump, _dash, _useShield, _isNotToFlip);

    //    _jump = false;

    //    _dash = false;

    //    _isNotToFlip = false;

    //}

    public float GetMove()
    {
        return _horizontalMove;
    }
}
