using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetObject : MonoBehaviour
{
    Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
    }

    public void Reset()
    {
        Debug.Log("reset");
        transform.position = _startPosition;
        GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
    }
}
