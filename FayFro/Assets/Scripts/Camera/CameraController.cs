using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _target = null;
    [SerializeField] private float _clamp = 0.1f;
    [SerializeField] private float _waitTime = 0.5f;

    private bool _canMove = false;
    private float _z;
    private bool _leftLimiter = false;
    private bool _rightLimiter = false;
    private bool _bottomLimiter = false;
    private bool _upperLimiter = false;

    const float _clampMax = 0.08f;

    public enum CameraDirection
    {
        Left,
        Right,
        Up,
        Down
    }

    void Start()
    {
        _z = this.transform.position.z;
        StartCoroutine(StartMoveCamera());
    }

    public void SetDirectionBool(CameraDirection direction, bool value)
    {
        switch(direction)
        {
            case CameraDirection.Left:
                {
                    _leftLimiter = value;
                    break;
                }
            case CameraDirection.Right:
                {
                    _rightLimiter = value;
                    break;
                }
            case CameraDirection.Up:
                {
                    _upperLimiter = value;
                    break;
                }
            case CameraDirection.Down:
                {
                    _bottomLimiter = value;
                    break;
                }
        }
    }

    private IEnumerator StartMoveCamera()
    {
        yield return new WaitForSeconds(_waitTime);
        _canMove = true;
    }


    void FixedUpdate()
    {

        if (_canMove == false)
        {
            return;
        }
        float moveX = _target.transform.position.x - gameObject.transform.position.x;
        float moveY = _target.transform.position.y - gameObject.transform.position.y;


        if (_leftLimiter == true && moveX < 0)
        {
            moveX = 0;
        }
        if(_rightLimiter == true && moveX > 0)
        {
            moveX = 0;
        }
        


        if (_upperLimiter == true && moveY > 0)
        {
            moveY = 0;
        }
        if(_bottomLimiter == true && moveY < 0)
        {
            moveY = 0;
        }

        moveX *= _clamp;
        moveY *= _clamp;
        
        if(moveX > _clampMax)
        {
            moveX = _clampMax;
        }
        else if(moveX < -_clampMax)
        {
            moveX = -_clampMax;
        }

        

        float positionX = gameObject.transform.position.x + moveX;
        float positionY = gameObject.transform.position.y + moveY;

        gameObject.transform.position = new Vector3(positionX, positionY, _z);
    }


    public void ChangeTarget(Transform target)
    {
        _target = target;
    }
}
