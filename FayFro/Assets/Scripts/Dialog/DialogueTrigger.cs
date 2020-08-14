using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private Dialogue _dialogue;

    public void TriggerDialogue()
    {
        Debug.Log("Dialogue trigger");
        FindObjectOfType<DialogueManager>().StartDialogue(_dialogue);
    }
}
