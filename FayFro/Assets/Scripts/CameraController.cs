using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    private float y;
    private float z;

    void Start()
    {
        y = this.transform.position.y;
        z = this.transform.position.z;
    }

    void Update()
    {
        float xDifference = player.position.x - this.transform.position.x;
        this.transform.position = new Vector3(this.transform.position.x + xDifference * 0.14f, y, z);
    }
}
