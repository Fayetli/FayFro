using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActivateObject : MonoBehaviour
{
    public abstract void Activate();
}

public class DialogueTrigger : ActivateObject
{
    [SerializeField] private Dialogue _dialogue;
    [SerializeField] private ActivateObject _activateObject = null;

    public override void Activate()
    {
        TriggerDialogue();
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(_dialogue, _activateObject);
    }
}
