using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private Text _nameText;
    [SerializeField] private Text _dialogueText;

    [SerializeField] private GameObject _dialogueObejct;

    private Queue<string> _sentences;
    private GameObject _player;

    void Start()
    {
        _sentences = new Queue<string>();
        _player = GameObject.FindObjectOfType<CharacterController2D>().gameObject;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        PlayerControllOff();

        _dialogueObejct.SetActive(true);

        _nameText.text = dialogue.name;

        _sentences.Clear();

        foreach(string sentence in dialogue._sentences)
        {
            _sentences.Enqueue(sentence);
        }

        DisplayNextSentence();

    }

    private void PlayerControllOff()
    {
        _player.GetComponent<CharacterController2D>().enabled = false;
        _player.GetComponent<PlayerMovement>().enabled = false;
        _player.GetComponent<BoxPlayerMover>().enabled = false;
    }

    private void PlayerControllOn()
    {
        _player.GetComponent<CharacterController2D>().enabled = true;
        _player.GetComponent<PlayerMovement>().enabled = true;
        _player.GetComponent<BoxPlayerMover>().enabled = true;
    }

    public void DisplayNextSentence()
    {
        if(_sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = _sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    private IEnumerator TypeSentence(string sentence)
    {
        _dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            _dialogueText.text += letter;
            yield return null;
        }
    }

    private void EndDialogue()
    {
        _dialogueObejct.SetActive(false);
        PlayerControllOn();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextSentence();
        }
    }

}
