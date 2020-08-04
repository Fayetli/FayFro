using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _player = null;


    [SerializeField] private float _xMinDifference = 0.1f;

    [SerializeField] private float _multiplier = 0.05f;

    private float _z;
    [SerializeField] private Transform _leftLimiter;
    [SerializeField] private Transform _rightLimiter;
    [SerializeField] private Transform _bottomLimiter;
    [SerializeField] private Transform _upperLimiter;

    void Start()
    {
        _z = this.transform.position.z;

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
            Mathf.Clamp(positionX, _leftLimiter.position.x, _rightLimiter.position.x),
            Mathf.Clamp(positionY, _bottomLimiter.position.y, _upperLimiter.position.y), _z);
    }
}
