using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMover : LinearMover
{

    [SerializeField] private bool FirstMoveOnRight = true;
    void Start()
    {
        if (OnStart)
        {
            StartCoroutine(HorizontalMoveObject());
        }
    }

    public void ActivateMove()
    {
        StartCoroutine(HorizontalMoveObject());
    }

    private IEnumerator HorizontalMoveObject()
    {
        if (!FirstMoveOnRight)
        {
            yield return StartCoroutine(MovingLeftCoroutine());
        }
        while (true)
        {
            yield return StartCoroutine(MovingRightCoroutine());
            yield return StartCoroutine(MovingLeftCoroutine());
        }

    }

    public IEnumerator MovingRightCoroutine()
    {
        float moveSpeed = _speed;
        float finishY = this.gameObject.transform.position.x + _distance;
        while (this.transform.position.x < finishY)
        {
            this.transform.position += Vector3.right * moveSpeed;
            moveSpeed *= _speedMulty;
            yield return new WaitForFixedUpdate();
        }

    }

    public IEnumerator MovingLeftCoroutine()
    {
        float moveSpeed = _speed;
        float finishY = this.gameObject.transform.position.x - _distance;
        while (this.transform.position.x > finishY)
        {
            this.transform.position -= Vector3.right * moveSpeed;
            moveSpeed *= _speedMulty;
            yield return new WaitForFixedUpdate();
        }

    }

}
