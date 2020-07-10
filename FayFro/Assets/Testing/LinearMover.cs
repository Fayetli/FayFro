using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearMover : MonoBehaviour
{
    [Tooltip("abs(module) value")]
    [SerializeField] protected float _distance = 0.0f;
    [SerializeField] protected float _speed = 0.0f;
    [SerializeField] protected float _speedMulty = 0.0f;
    [SerializeField] protected bool OnStart = false;
}
