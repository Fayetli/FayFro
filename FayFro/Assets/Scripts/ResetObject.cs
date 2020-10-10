using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetObject : MonoBehaviour
{
    protected Vector3 _startPosition = new Vector3(0, 0, 0);

    [SerializeField] protected GameObject _destroyAnimationObject = null;

    private void Awake()
    {
        _startPosition = transform.position;

    }

    public void Reset()
    {
        GameObject player = GameObject.FindObjectOfType<CharacterController2D>().gameObject;

        Vector2 distanceVector = player.transform.position - transform.position;
        const float maxDistance = 2.0f;

        float distance = Mathf.Sqrt(Mathf.Pow(distanceVector.x, 2) + Mathf.Pow(distanceVector.y, 2));

        if (distance < maxDistance)
        {
            float vectorMultyplier = Mathf.Pow(maxDistance, 2) / distance;
            Vector2 maxForceVector = new Vector2(vectorMultyplier * distanceVector.x, vectorMultyplier * distanceVector.y);
            Vector2 forceVector = maxForceVector - distanceVector;
            player.GetComponent<Rigidbody2D>().AddForce(forceVector * 250.0f);

            Debug.Log("md: " + maxDistance);
            Debug.Log("dV: " + distanceVector);
            Debug.Log("d: " + distance);
            Debug.Log("alpha: " + vectorMultyplier);
            Debug.Log("fV: " + forceVector * 250);
        }

        GameObject boxAnimObj = Instantiate(_destroyAnimationObject);
        boxAnimObj.transform.position = gameObject.transform.position;

        gameObject.transform.position = _startPosition;


      
    }


    public void Destroy()
    {
        GameObject boxAnimObj = Instantiate(_destroyAnimationObject);
        boxAnimObj.transform.position = gameObject.transform.position;

        Destroy(gameObject);
    }
}
