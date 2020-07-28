using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LinearMover : MonoBehaviour
{
    [Tooltip("abs(module) value")]
    [SerializeField] protected float _distance = 0.0f;
    [SerializeField] protected float _speed = 0.0f;
    [SerializeField] protected float _speedMulty = 0.0f;
    [SerializeField] protected bool OnStart = false;
    [SerializeField] protected float beetwenWaitTime = 0.0f;
    [SerializeField] protected float waitTime = 0.0f;
    protected bool _stopMove = false;

    public abstract void ActivateMove();
    public abstract void StopMove();
    public abstract void ContinueMove();

}
