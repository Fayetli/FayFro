using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform _player = null;


    [SerializeField] private float _xMinDifference = 0.1f;
    //[SerializeField] private float _yMinDifference = 0.1f;//DONT USE

    [SerializeField] private float _multiplier = 0.05f;

    private float _z;
    private float _leftLimiter;
    private float _rightLimiter;
    private float _bottomLimiter;
    private float _upperLimiter;

    void Start()
    {
        _z = this.transform.position.z;

        _leftLimiter = GameObject.Find("Left Limiter").transform.position.x;
        _rightLimiter = GameObject.Find("Right Limiter").transform.position.x;
        _upperLimiter = GameObject.Find("Up Limiter").transform.position.y;
        _bottomLimiter = GameObject.Find("Down Limiter").transform.position.y;
        _player = GameObject.Find("Player").transform;
    }

    float positionX;
    float positionY;
    void FixedUpdate()
    {
        if (Mathf.Abs(_player.position.x - this.transform.position.x) < _xMinDifference)
        {
            positionX = this.transform.position.x;
        }
        else
        {
            positionX = this.transform.position.x + (_player.position.x - this.transform.position.x) * _multiplier;
        }

        if (Mathf.Abs(_player.position.y - this.transform.position.y) < _xMinDifference)
        {
            positionY = this.transform.position.y;
        }
        else
        {
            positionY = this.transform.position.y + (_player.position.y - this.transform.position.y) * _multiplier;

        }

        this.transform.position = new Vector3(
            Mathf.Clamp(positionX, _leftLimiter, _rightLimiter),
            Mathf.Clamp(positionY, _bottomLimiter, _upperLimiter), _z);
    }
}
