using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalBouncer : MonoBehaviour
{
    [SerializeField] private bool BounceUp = true;
    [SerializeField] private float _bounceForce = 750f;

    private Collider2D _bounceObject;
    private bool _onBouncer = false;

    private void Start()
    {
        if(BounceUp == false)
        {
            _bounceForce *= -1;
        }
    }
    private IEnumerator Bounce()
    {
        yield return new WaitForSeconds(0.5f);

        if (_onBouncer)
        {
            gameObject.GetComponent<Animator>().SetBool("push", true);
            Rigidbody2D rigidBody = _bounceObject.GetComponent<Rigidbody2D>();
            rigidBody.AddForce(new Vector2(0f, _bounceForce * rigidBody.mass));
            rigidBody.velocity = new Vector3(0, 0, 0);
        }
        _bounceObject = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<BounceObject>() != null)
        {
            _bounceObject = collision;
            StartCoroutine(Bounce());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<BounceObject>() != null)
        {
            _onBouncer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<BounceObject>() != null)
        {
            _onBouncer = false;
            _bounceObject = null;
            gameObject.GetComponent<Animator>().SetBool("push", false);
        }
    }
}
