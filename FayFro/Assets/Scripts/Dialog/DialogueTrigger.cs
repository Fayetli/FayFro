using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class ActivateObject : MonoBehaviour
{
    public abstract void Activate();
}

public class DialogueTrigger : ActivateObject
{
    [SerializeField] private Dialogue _dialogue;
    [SerializeField] private ActivateObject _activateObject = null;

    public UnityEvent OnActivateDialogue;

    private void Start()
    {
        if(OnActivateDialogue == null)
        {
            OnActivateDialogue = new UnityEvent();
        }
    }

    public override void Activate()
    {
        TriggerDialogue();
    }

    public void TriggerDialogue()
    {
        OnActivateDialogue.Invoke();
        FindObjectOfType<DialogueManager>().StartDialogue(_dialogue, _activateObject);
    }
}
