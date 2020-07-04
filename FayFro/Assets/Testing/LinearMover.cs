using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearMover : MonoBehaviour
{
    [Tooltip("abs(module) value")]
    [SerializeField] private float _distance = 0.0f;
    [SerializeField] private float _speed = 0.0f;
    [SerializeField] private float _speedMulty = 0.0f;

    [SerializeField] private bool _isUpMoving = true;
    public void ChangeVectorMoving()
    {
        _isUpMoving = (_isUpMoving) ? false : true;
    }
    public void MoveObject()
    {
        if (_isUpMoving)
        {
            StartCoroutine(MovingUpCoroutine());
        }
        else
        {
            StartCoroutine(MovingDownCoroutine());
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
        ChangeVectorMoving();

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
        ChangeVectorMoving();

    }
}
