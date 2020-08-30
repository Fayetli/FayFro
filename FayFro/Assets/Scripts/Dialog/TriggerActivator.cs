using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerActivator : MonoBehaviour
{
    [SerializeField] private ActivateСondition _condition = ActivateСondition.OnTriggerEnter;
    [SerializeField] private OffAfterActivate _offMode = OffAfterActivate.Object;
    [SerializeField] private string _layer;
    public UnityEvent OnActivate;

    enum ActivateСondition
    {
        OnTriggerEnter,
        OnButtonDown
    }

    enum OffAfterActivate
    {
        Object,
        ColliderComponent,
        Activator
    }



    private void Start()
    {
        if(OnActivate == null)
        {
            OnActivate = new UnityEvent();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_condition != ActivateСondition.OnTriggerEnter)
            return;

        TryToInvokeEvent(collision);

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            TryToInvokeEvent(collision);
        }

    }


    private void TryToInvokeEvent(Collider2D collision)
    {
        if (_layer == "" || LayerMask.NameToLayer(_layer) == collision.gameObject.layer)
        {
            InvokeEvent();
        }
    }

    private void InvokeEvent()
    {
        OnActivate.Invoke();

        switch (_offMode)
        {
            case OffAfterActivate.Object:
                {
                    gameObject.SetActive(false);
                    break;
                }
            case OffAfterActivate.ColliderComponent:
                {
                    gameObject.GetComponent<Collider2D>().enabled = false;
                    break;
                }
            case OffAfterActivate.Activator:
                {
                    this.enabled = false;
                    break;
                }
        }
    }
}
