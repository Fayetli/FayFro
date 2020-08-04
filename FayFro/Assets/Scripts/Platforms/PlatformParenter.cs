using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformParenter : MonoBehaviour
{
    private Transform _startParentTransform;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<ChilderObject>() != null)
        {
            _startParentTransform = collision.transform.parent;
            collision.transform.parent = transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<ChilderObject>() != null)
        {
            collision.transform.parent = _startParentTransform;
        }
    }
}
