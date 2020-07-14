using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [SerializeField] private float _timeToAutoDestroy = 1f;
    void Start()
    {
        Invoke("AutoDstroy", _timeToAutoDestroy);
    }

    private void AutoDstroy()
    {
        Destroy(gameObject);
    }
}
