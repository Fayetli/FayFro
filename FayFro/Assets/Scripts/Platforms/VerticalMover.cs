﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMover : LinearMover
{
    [SerializeField] private bool FirstMoveOnUp = true;

    private Vector3 _startPosition;
    void Start()
    {
        _startPosition = gameObject.transform.position;

        DeadZone.OnReset += Reset;

        if (OnStart)
        {
            StartCoroutine(VerticalMoveObject());
        }
    }

    public void ChangeDistance(float distance)
    {
        _distance = distance;
    }

    public override void ActivateMove()
    {
        StartCoroutine(VerticalMoveObject());
    }

    public override void StopMove()
    {
        _stopMove = true;
    }

    public override void ContinueMove()
    {
        _stopMove = false;
    }
    private IEnumerator VerticalMoveObject()
    {
        yield return new WaitForSeconds(waitTime);
        if (!FirstMoveOnUp)
        {
            yield return StartCoroutine(MovingDownCoroutine());
            yield return new WaitForSeconds(beetwenWaitTime);
        }
        while (true) {
            while (_stopMove && FirstMoveOnUp)
            {
                yield return null;
                
            }

            yield return StartCoroutine(MovingUpCoroutine());
            yield return new WaitForSeconds(beetwenWaitTime);

            while (_stopMove && !FirstMoveOnUp)
            {
                yield return null;
                
            }

            yield return StartCoroutine(MovingDownCoroutine());
            yield return new WaitForSeconds(beetwenWaitTime);
        }
    }

    public IEnumerator MovingUpCoroutine()
    {
        float moveSpeed = _speed;
        float finishY = this.gameObject.transform.localPosition.y + _distance;
        while (this.transform.localPosition.y < finishY)
        {
            this.transform.localPosition += Vector3.up * moveSpeed;
            moveSpeed *= _speedMulty;
            yield return new WaitForFixedUpdate();
        }

    }

    public IEnumerator MovingDownCoroutine()
    {
        float moveSpeed = _speed;
        float finishY = this.gameObject.transform.localPosition.y - _distance;
        while (this.transform.localPosition.y > finishY)
        {
            this.transform.localPosition -= Vector3.up * moveSpeed;
            moveSpeed *= _speedMulty;
            yield return new WaitForFixedUpdate();
        }
        
    }

    private void Reset()
    {
        StopAllCoroutines();
        gameObject.transform.position = _startPosition;
        _stopMove = false;
        if (OnStart)
            StartCoroutine(VerticalMoveObject());
    }

    private void OnDisable()
    {
        DeadZone.OnReset -= Reset;
    }
}
