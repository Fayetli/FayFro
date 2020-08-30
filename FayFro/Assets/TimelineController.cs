using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimelineController : MonoBehaviour
{
    public UnityEvent OnAwake;
    public UnityEvent OnSleep;

    private GameObject _player;
    private void Awake()
    {
        Debug.Log("Off");
        if(OnAwake == null)
        {
            OnAwake = new UnityEvent();
        }
        if(OnSleep == null)
        {
            OnSleep = new UnityEvent();
        }
        OnAwake.Invoke();

        _player = GameObject.FindObjectOfType<CharacterController2D>().gameObject;
        if (_player == null)
            return;

        _player.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        _player.gameObject.GetComponent<PlayerMovement>().enabled = false;
        _player.gameObject.GetComponent<CharacterController2D>().enabled = false;
        _player.gameObject.GetComponent<BoxPlayerMover>().enabled = false;
    }

    private void OnDisable()
    {
        Debug.Log("On");
        OnSleep.Invoke();

        if (_player == null || GameObject.FindObjectOfType<DialogueBox>() != null)
            return;

        _player.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        _player.gameObject.GetComponent<PlayerMovement>().enabled = true;
        _player.gameObject.GetComponent<CharacterController2D>().enabled = true;
        _player.gameObject.GetComponent<BoxPlayerMover>().enabled = true;
    }
}
