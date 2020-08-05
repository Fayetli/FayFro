using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : ResetObject, BounceObject, PlatformerObject, IChilderObject
{
    Transform _startParent;

    private void Start()
    {
        _startParent = gameObject.transform.parent;
    }
    public void SetStartParent()
    {
        gameObject.transform.parent = _startParent;
    }
}
