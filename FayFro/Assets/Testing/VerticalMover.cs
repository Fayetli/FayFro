using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMover : LinearMover
{
    [SerializeField] private bool FirstMoveOnUp = true;
    void Start()
    {
        if (OnStart)
        {
            StartCoroutine(VerticalMoveObject());
        }
    }

    public void ActivateMove()
    {
        StartCoroutine(VerticalMoveObject());
    }

    private IEnumerator VerticalMoveObject()
    {
        if (!FirstMoveOnUp)
        {
            yield return StartCoroutine(MovingDownCoroutine());
        }
        while (true) {
            yield return StartCoroutine(MovingUpCoroutine());
            yield return StartCoroutine(MovingDownCoroutine());
        }
    }

    public IEnumerator MovingUpCoroutine()
    {
        float moveSpeed = _speed;
        float finishY = this.gameObject.transform.position.y + _distance;
        while (this.transform.position.y < finishY)
        {
            this.transform.position += Vector3.up * moveSpeed;
            moveSpeed *= _speedMulty;
            yield return new WaitForFixedUpdate();
        }

    }

    public IEnumerator MovingDownCoroutine()
    {
        float moveSpeed = _speed;
        float finishY = this.gameObject.transform.position.y - _distance;
        while (this.transform.position.y > finishY)
        {
            this.transform.position -= Vector3.up * moveSpeed;
            moveSpeed *= _speedMulty;
            yield return new WaitForFixedUpdate();
        }

    }
}
