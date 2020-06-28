using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    private float z;

    public CameraLimiter LeftLimiter;
    public CameraLimiter RightLimiter;
    public CameraLimiter UpLimiter;
    public CameraLimiter DownLimiter;

    void Start()
    {
        z = this.transform.position.z;

        LeftLimiter = GameObject.Find("Left Limiter").GetComponent<CameraLimiter>();
        RightLimiter = GameObject.Find("Right Limiter").GetComponent<CameraLimiter>();
        UpLimiter = GameObject.Find("Up Limiter").GetComponent<CameraLimiter>();
        DownLimiter = GameObject.Find("Down Limiter").GetComponent<CameraLimiter>();

    }

    void Update()
    {
        
        float xDifference = player.position.x - this.transform.position.x;
        if (xDifference < 0 && LeftLimiter.isVisible == true)
        {
            xDifference = 0;
        }
        else if (xDifference > 0 && RightLimiter.isVisible == true)
        {
            xDifference = 0;
        }
        float yDifference = player.position.y - this.transform.position.y;
        if (yDifference < 0 && DownLimiter.isVisible == true)
        {
            yDifference = 0;
        }
        else if (yDifference > 0 && UpLimiter.isVisible == true)
        {
            yDifference = 0;
        }
        this.transform.position = new Vector3(this.transform.position.x + xDifference * 0.14f, this.transform.position.y + yDifference * 0.14f, z);
    }
}
