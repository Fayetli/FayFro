using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearMover : MonoBehaviour
{
    private float _startY;
    [Tooltip("abs(module) value")]
    [SerializeField] private float _distance = 1;
    [SerializeField] private int _moveCount = 50;



    [SerializeField] private bool _isUpMoving;

    public void ChangeVectorMoving()
    {
        _isUpMoving = (_isUpMoving) ? false : true;
    }
    public void MoveObject()
    {
        _startY = this.transform.position.y;
        if (_isUpMoving)
        {
            StartCoroutine(MovingUpCoroutine(_distance / _moveCount, _startY + _distance));
        }
        else
        {
            StartCoroutine(MovingDownCoroutine(_distance / _moveCount * -1, _startY + _distance));
        }
    }

    IEnumerator MovingUpCoroutine(float oneMove, float finishY)
    {
        while(this.transform.position.y < finishY)
        {
            this.transform.position += Vector3.up * oneMove;
            yield return new WaitForFixedUpdate();
        }
        ChangeVectorMoving();
    }

    IEnumerator MovingDownCoroutine(float oneMove, float finishY)
    {
        finishY *= -1;
        while (this.transform.position.y > finishY)
        {
            this.transform.position += Vector3.up * oneMove;
            yield return new WaitForFixedUpdate();
        }
        ChangeVectorMoving();
    }
}
