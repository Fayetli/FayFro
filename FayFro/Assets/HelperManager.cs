using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperManager : MonoBehaviour
{
    [SerializeField] private GameObject _activateObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<CharacterController2D>() != null)
        {
            _activateObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<CharacterController2D>() != null)
        {
            _activateObject.SetActive(false);
        }
    }

    public void Off()
    {
        if (_activateObject != null)
        {
            _activateObject.SetActive(false);
            _activateObject = null;
        }
        Destroy(this);
    }

}
