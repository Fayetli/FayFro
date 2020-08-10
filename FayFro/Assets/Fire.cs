using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private float _addVelocityX;

    [SerializeField] private float _minVelocityYRange;
    [SerializeField] private float _maxVelocityYRange;

    [SerializeField] private float _minTimeToDestroy;
    [SerializeField] private float _maxTimeToDestroy;

    private float _randomVelocityY;
    void Start()
    {
        float timeToDestroy = Random.Range(_minTimeToDestroy, _maxTimeToDestroy);
        _randomVelocityY = Random.Range(_minVelocityYRange, _maxVelocityYRange);
        StartCoroutine(Firing(timeToDestroy));
    }

    private IEnumerator Firing(float timeToDestroy)
    {
        Rigidbody2D rigidbody = gameObject.GetComponent<Rigidbody2D>();

        for (float i = 1; i > 0.2f; i-= 0.1f)
        {
            rigidbody.velocity = new Vector2(_addVelocityX * i, _randomVelocityY);
            yield return new WaitForSeconds(timeToDestroy / 7);

        }
        DestroyFire();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<CharacterController2D>() != null)
        {
            Debug.Log("Destroy Player");
        }
        else if(collision.gameObject.GetComponent<Firegun>() == null && collision.gameObject.GetComponent<Fire>() == null)
        {
            Destroy(gameObject);
        }
    }


    private void DestroyFire()
    {
        Destroy(gameObject);

    }

}
