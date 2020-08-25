using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlayerStayVerticalMover : MonoBehaviour
{
    [SerializeField] private bool FirstMoveOnUp = true;

    [Tooltip("abs(module) value")]
    [SerializeField] protected float _distance = 0.0f;
    [SerializeField] protected float _speed = 0.0f;
    [SerializeField] protected float _speedMulty = 0.0f;
    [SerializeField] protected float waitTime = 0.0f;
    void Start()
    {
        StartCoroutine(VerticalMoveObject());
    }


    private bool _playerStay = false;



    private IEnumerator VerticalMoveObject()
    {
        while (true)
        {
            if (_playerStay == false)
            {
                yield return null;
                continue;
            }
            if (FirstMoveOnUp)
            {
                yield return new WaitForSeconds(waitTime);
                yield return StartCoroutine(MovingUpCoroutine());
                yield return StartCoroutine(MovingDownCoroutine());
            }
            else
            {
                yield return new WaitForSeconds(waitTime);
                yield return StartCoroutine(MovingDownCoroutine());
                yield return StartCoroutine(MovingUpCoroutine());
            }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<CharacterController2D>() != null)
        {
            _playerStay = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<CharacterController2D>() != null)
        {
            _playerStay = false;
        }
    }
}
