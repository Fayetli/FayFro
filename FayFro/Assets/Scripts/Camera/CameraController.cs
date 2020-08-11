using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _player = null;
    [SerializeField] private float _clamp = 0.05f;
    //[SerializeField] private float _waitTime = 0.1f;

    private bool _canMove = false;
    private float _z;
    private bool _leftLimiter = false;
    private bool _rightLimiter = false;
    private bool _bottomLimiter = false;
    private bool _upperLimiter = false;


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
        //StartCoroutine(StartMoveCamera());
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

    //private IEnumerator StartMoveCamera()
    //{
    //    yield return new WaitForSeconds(_waitTime);
    //    _canMove = true;
    //}

    
    void FixedUpdate()
    {

        //if(_canMove == false)
        //{
        //    return;
        //}
        float moveX = _player.transform.position.x - gameObject.transform.position.x;
        float moveY = _player.transform.position.y - gameObject.transform.position.y;

        

        Debug.Log("LeftLimiter: " + _leftLimiter);
        Debug.Log("RightLimiter: " + _rightLimiter);
        Debug.Log("UpperLimiter: " + _upperLimiter);
        Debug.Log("BottomLimiter: " + _bottomLimiter);
        


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
        
        if(moveX > 0.04f)
        {
            moveX = 0.04f;
        }
        else if(moveX < -0.04f)
        {
            moveX = -0.04f;
        }

        

        float positionX = gameObject.transform.position.x + moveX;
        float positionY = gameObject.transform.position.y + moveY;

        gameObject.transform.position = new Vector3(positionX, positionY, _z);
    }
}
