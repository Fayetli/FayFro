using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerActivator : MonoBehaviour
{
    [SerializeField] private ActivateObject _dialogue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _dialogue.Activate();

        gameObject.SetActive(false);
    }
}
