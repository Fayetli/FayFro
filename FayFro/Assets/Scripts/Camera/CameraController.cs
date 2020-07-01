using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;

    private float z;

    private float xMinDifference = 0.1f;
    private float yMinDifference = 0.1f;

    private float multiplier = 0.05f;

    private float _leftLimiter;
    private float _rightLimiter;
    private float _bottomLimiter;
    private float _upperLimiter;

    void Start()
    {
        z = this.transform.position.z;

        _leftLimiter = GameObject.Find("Left Limiter").transform.position.x;
        _rightLimiter = GameObject.Find("Right Limiter").transform.position.x;
        _upperLimiter = GameObject.Find("Up Limiter").transform.position.y;
        _bottomLimiter = GameObject.Find("Down Limiter").transform.position.y;

    }

    float positionX;
    float positionY;
    void FixedUpdate()
    {
        if (Mathf.Abs(player.position.x - this.transform.position.x) < xMinDifference)
        {
            positionX = this.transform.position.x;
        }
        else
        {
            positionX = this.transform.position.x + (player.position.x - this.transform.position.x) * multiplier;
        }

        if (Mathf.Abs(player.position.y - this.transform.position.y) < yMinDifference)
        {
            positionY = this.transform.position.y;
        }
        else
        {
            positionY = this.transform.position.y + (player.position.y - this.transform.position.y) * multiplier;

        }

        this.transform.position = new Vector3(
            Mathf.Clamp(positionX, _leftLimiter, _rightLimiter),
            Mathf.Clamp(positionY, _bottomLimiter, _upperLimiter), z);
    }
}
