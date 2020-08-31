using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeActivator : MonoBehaviour
{
    public UnityEvent OnActivate;
    [SerializeField] private float _waitTime;
    [SerializeField] private bool ActivateOnStart = true;

    private void Start()
    {
        if(OnActivate == null)
        {
            OnActivate = new UnityEvent();
        }

        if (ActivateOnStart)
        {
            StartCoroutine(Activate(_waitTime));
        }
    }

    public IEnumerator Activate(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        OnActivate.Invoke();
    }
}
