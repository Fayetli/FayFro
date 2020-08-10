using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _player = null;

    private float _z;
    private bool _leftLimiter;
    private bool _rightLimiter;
    private bool _bottomLimiter;
    private bool _upperLimiter;

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


    private float _positionX;
    private float _positionY;
    [SerializeField] private float _clamp;
    void FixedUpdate()
    {
        float moveX =  _player.transform.position.x - gameObject.transform.position.x;

        if (_leftLimiter && moveX < 0)
        {
            moveX = 0;
        }
        if(_rightLimiter && moveX > 0)
        {
            moveX = 0;
        }

        float moveY = _player.transform.position.y - gameObject.transform.position.y;

        
        if(_upperLimiter && moveY > 0)
        {
            moveY = 0;
        }
        if(_bottomLimiter && moveY < 0)
        {
            moveY = 0;
        }

        int dirRight = 1;
        if (moveX < 0)
        {
            dirRight = -1;
        }

        
        float maxMove = 0.08f;
        moveX *= _clamp;
        moveY *= _clamp;

        if (Mathf.Abs(moveX) > maxMove)
        {
            moveX = maxMove * dirRight;
        }
        if(Mathf.Abs(moveY) > maxMove)
        {
            moveY = maxMove * dirRight;
        }

        _positionX = gameObject.transform.position.x + moveX;
        _positionY = gameObject.transform.position.y + moveY;

        gameObject.transform.position = new Vector3(_positionX, _positionY, _z);
    }
}
