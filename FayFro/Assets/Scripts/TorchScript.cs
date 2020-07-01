using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchScript : MonoBehaviour
{



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
            if (this.GetComponent<Animator>().GetBool("isActive") == false)
                this.GetComponent<Animator>().SetBool("isActive", true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Player")
            if (this.GetComponent<Animator>().GetBool("isActive") == true)
                this.GetComponent<Animator>().SetBool("isActive", false);
    }
}
