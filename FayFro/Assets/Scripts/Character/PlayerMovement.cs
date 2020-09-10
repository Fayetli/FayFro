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

    private float _runSpeed = 18f;

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

    public void StopMove()
    {
        StopCoroutine(TrackingKeys());
        _horizontalMove = 0f;
        _jump = _dash = _useShield = false;
        _controller.Move(_horizontalMove, _jump, _dash, _useShield);
    }

    public void ContinueMove()
    {
        StartCoroutine(TrackingKeys());
    }

    IEnumerator TrackingKeys() {

        while (true)
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
            yield return null;
        }
    }

    void FixedUpdate()
    {

        _controller.Move(_horizontalMove * Time.fixedDeltaTime, _jump, _dash, _useShield);

        _jump = false;

        _dash = false;

    }

    private void OnDisable()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
    }


}
