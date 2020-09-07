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
    public UnityEvent OnDeactivate;

    enum ActivateСondition
    {
        OnTriggerEnter,
        OnButtonDown
    }

    enum OffAfterActivate
    {
        Object,
        ColliderComponent,
        Activator,
        None
    }



    private void Start()
    {
        if(OnActivate == null)
        {
            OnActivate = new UnityEvent();
        }
        if(OnDeactivate == null)
        {
            OnDeactivate = new UnityEvent();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_condition != ActivateСondition.OnTriggerEnter)
            return;

        TryToInvokeEvent(collision, OnActivate);

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            TryToInvokeEvent(collision, OnActivate);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        TryToInvokeEvent(collision, OnDeactivate);
    }


    private void TryToInvokeEvent(Collider2D collision, UnityEvent currentEvent)
    {
        if (_layer == "" || LayerMask.NameToLayer(_layer) == collision.gameObject.layer)
        {
            InvokeEvent(currentEvent);
        }
    }



    private void InvokeEvent(UnityEvent currentEvent)
    {
        currentEvent.Invoke();

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
