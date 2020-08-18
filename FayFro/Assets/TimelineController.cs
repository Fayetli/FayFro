using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineController : MonoBehaviour
{
    private void Awake()
    {
        GameObject player = GameObject.FindObjectOfType<CharacterController2D>().gameObject;

        player.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        player.gameObject.GetComponent<PlayerMovement>().enabled = false;
        player.gameObject.GetComponent<CharacterController2D>().enabled = false;
        player.gameObject.GetComponent<BoxPlayerMover>().enabled = false;
    }

    private void OnDisable()
    {
        GameObject player = GameObject.FindObjectOfType<CharacterController2D>().gameObject;

        player.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        player.gameObject.GetComponent<PlayerMovement>().enabled = true;
        player.gameObject.GetComponent<CharacterController2D>().enabled = true;
        player.gameObject.GetComponent<BoxPlayerMover>().enabled = true;
    }
}
