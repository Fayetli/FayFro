using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalBouncer : MonoBehaviour
{
    [SerializeField] private bool BounceRight = true;
    [SerializeField] private float _bounceForce = 750f;
    [SerializeField] private bool _withPause = true;

    private Collider2D _bounceObject;
    private bool _onBouncer = false;

    private void Start()
    {
        if (BounceRight == false)
        {
            _bounceForce *= -1;
        }
    }
    private IEnumerator Bounce()
    {
        Debug.Log("Bounce");
        if (_withPause)
        {
            yield return new WaitForSeconds(0.5f);
        }
        if (_onBouncer)
        {
            Debug.Log("Take Bounce");
            Rigidbody2D rigidBody = _bounceObject.GetComponent<Rigidbody2D>();
            rigidBody.AddForce(new Vector2(_bounceForce * rigidBody.mass, 0f));
            rigidBody.velocity = new Vector3(0, 0, 0);
        }
        _bounceObject = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enter");
        if (collision.gameObject.GetComponent<BounceObject>() != null)
        {
            _bounceObject = collision;
            _onBouncer = true;
            StartCoroutine(Bounce());
        }
    }

    

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<BounceObject>() != null)
        {
            _onBouncer = false;
        }
    }
}
