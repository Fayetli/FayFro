using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour, BounceObject, PlatformerObject
{
    private Transform startTransform;

    private void Start()
    {
        startTransform = gameObject.transform;
    }

    
}
