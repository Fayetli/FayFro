using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformParenter : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<IChilderObject>() != null)
        {
            if(collision.gameObject.GetComponent<Box>() == null || collision.gameObject.transform.parent.GetComponent<CharacterController2D>() == null)
            { 
                collision.transform.parent = transform;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<IChilderObject>() != null)
        {
            if (collision.gameObject.GetComponent<Box>() == null || collision.gameObject.transform.parent.GetComponent<CharacterController2D>() == null)
            {
                collision.gameObject.GetComponent<IChilderObject>().SetStartParent();
            }
        }
    }
}
