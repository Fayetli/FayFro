﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinityLinearMover : MonoBehaviour
{
    [SerializeField] protected float _speed = 0.0f;
    [SerializeField] protected bool OnStart = false;
    [SerializeField] protected GameObject _moveObjectPref;
    [SerializeField] protected int _objectCount;
    [SerializeField] protected float _objectDistance;

    protected List<GameObject> _moveObjects;
    void Start()
    {
        
    }




}