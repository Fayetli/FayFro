using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController2D), typeof(BoxPlayerMover))]
public class PlayerMovement : MonoBehaviour, PlatformerObject, IChilderObject
{
    private CharacterController2D _controller;

    [SerializeField] private bool _HaveTP = false;

    [SerializeField] private bool _HaveShield = false;

    [SerializeField] private bool _HaveDoubleJump = false;

    private float _horizontalMove = 0f;

    private float _runSpeed = 23f;

    private bool _jump = false;


    private bool _dash = false;

    private bool _useShield = false;

    Transform _startParent;
    private void Start()
    {
        _controller = GetComponent<CharacterController2D>();
        _startParent = gameObject.transform.parent;
        if (_HaveDoubleJump)
        {
            _controller.SetHaveDoubleJumpTrue();
        }
    }

    public void SetStartParent()
    {
        gameObject.transform.parent = _startParent;
    }

    private void Update()
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
    }
    void FixedUpdate()
    {

        _controller.Move(_horizontalMove * Time.fixedDeltaTime, _jump, _dash, _useShield);

        _jump = false;

        _dash = false;

    }


}
