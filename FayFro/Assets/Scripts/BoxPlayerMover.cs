﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPlayerMover : MonoBehaviour
{
    [SerializeField] private Transform _boxOverlap = null;
    [SerializeField] private LayerMask _whatIsBox = 0;

    private GameObject _box;
    private Transform _startBoxParent;
    public bool IsBox = false;
    private CharacterController2D _controller;


    private void Start()
    {
        _controller = gameObject.GetComponent<CharacterController2D>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("IS Z");
            Vector2 postion = new Vector2(_boxOverlap.transform.position.x, _boxOverlap.transform.position.y);
            Collider2D hit = Physics2D.OverlapCircle(postion, 0.25f, _whatIsBox);
            if (hit != null && hit.gameObject.GetComponent<Box>() != null)
            {
                _box = hit.gameObject;
                _startBoxParent = _box.gameObject.transform.parent;
                _box.transform.parent = gameObject.transform;
                _box.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                IsBox = true;
            }

        }
        if (Input.GetKey(KeyCode.Z))
        {


            if (_box != null)
            {
                Vector2 postion = new Vector2(_boxOverlap.transform.position.x, _boxOverlap.transform.position.y);
                Collider2D hit = Physics2D.OverlapCircle(postion, 0.25f, _whatIsBox);

                if (hit == null || hit.gameObject.GetComponent<Box>() == null || _controller.IsGrounded() == false)
                {
                    _box.gameObject.transform.parent = _startBoxParent;
                    _box.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                    _box = null;
                    IsBox = false;
                }

            }
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            Debug.Log("IS NOT Z");
            if (_box != null)
            {
                _box.gameObject.transform.parent = _startBoxParent;
                _box.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                _startBoxParent = null;
                _box = null;
                IsBox = false;
            }
        }


    }




}
