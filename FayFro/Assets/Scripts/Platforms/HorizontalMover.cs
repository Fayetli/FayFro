using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMover : LinearMover
{

    [SerializeField] private bool FirstMoveOnRight = true;

    private Vector3 _startPosition;
   
    void Start()
    {
        _startPosition = gameObject.transform.position;

        DeadZone.OnReset += Reset;

        if (OnStart)
        {
            StartCoroutine(HorizontalMoveObject());
        }
    }
    public override void StopMove()
    {
        _stopMove = true;
    }

    public override void ContinueMove()
    {
        _stopMove = false;
    }

    public override void ActivateMove()
    {
        StartCoroutine(HorizontalMoveObject());
    }

    private IEnumerator HorizontalMoveObject()
    {

        yield return new WaitForSeconds(waitTime);
        if (!FirstMoveOnRight)
        {
            yield return StartCoroutine(MovingLeftCoroutine());
            yield return new WaitForSeconds(beetwenWaitTime);
        }
        while (true)
        {
            while (_stopMove && FirstMoveOnRight)
            {
                yield return null;

            }

            yield return StartCoroutine(MovingRightCoroutine());
            yield return new WaitForSeconds(beetwenWaitTime);

            while (_stopMove && FirstMoveOnRight)
            {
                yield return null;

            }

            yield return StartCoroutine(MovingLeftCoroutine());
            yield return new WaitForSeconds(beetwenWaitTime);
        }

    }

    public IEnumerator MovingRightCoroutine()
    {
        float moveSpeed = _speed;
        float finishY = this.gameObject.transform.localPosition.x + _distance;
        while (this.transform.localPosition.x < finishY)
        {
            this.transform.localPosition += Vector3.right * moveSpeed;
            moveSpeed *= _speedMulty;
            yield return new WaitForFixedUpdate();
        }

    }

    public IEnumerator MovingLeftCoroutine()
    {
        float moveSpeed = _speed;
        float finishY = this.gameObject.transform.localPosition.x - _distance;
        while (this.transform.localPosition.x > finishY)
        {
            this.transform.localPosition -= Vector3.right * moveSpeed;
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
            StartCoroutine(HorizontalMoveObject());
    }

    private void OnDisable()
    {
        DeadZone.OnReset -= Reset;
    }


}
