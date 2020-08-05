using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMover : LinearMover
{

    [SerializeField] private bool FirstMoveOnRight = true;
    
    
    public override void StopMove()
    {
        _stopMove = true;
    }

    public override void ContinueMove()
    {
        _stopMove = false;
    }
    void Start()
    {
        if (OnStart)
        {
            StartCoroutine(HorizontalMoveObject());
        }
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
            if (!_stopMove)
            {
                yield return StartCoroutine(MovingRightCoroutine());
                yield return new WaitForSeconds(beetwenWaitTime);
                yield return StartCoroutine(MovingLeftCoroutine());
                yield return new WaitForSeconds(beetwenWaitTime);
            }
            else
            {
                yield return null;
            }
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

}
