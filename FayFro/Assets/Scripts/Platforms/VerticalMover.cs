using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMover : LinearMover
{
    [SerializeField] private bool FirstMoveOnUp = true;
    [SerializeField] private float waitTime = 0.0f;
    [SerializeField] private float beetwenWaitTime = 0.0f;
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
        yield return new WaitForSeconds(waitTime);
        if (!FirstMoveOnUp)
        {
            yield return StartCoroutine(MovingDownCoroutine());
            yield return new WaitForSeconds(beetwenWaitTime);
        }
        while (true) {
            yield return StartCoroutine(MovingUpCoroutine());
            yield return new WaitForSeconds(beetwenWaitTime);
            yield return StartCoroutine(MovingDownCoroutine());
            yield return new WaitForSeconds(beetwenWaitTime);
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
