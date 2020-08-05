using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetObject : MonoBehaviour
{
    private Vector3 _startPosition = new Vector3(0, 0, 0);

    [SerializeField] private GameObject _destroyAnimationObject = null;

    private void Awake()
    {
        _startPosition = transform.position;
        Debug.Log(_startPosition);
    }

    public void Reset()
    {
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
