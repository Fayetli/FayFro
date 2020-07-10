using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinityVerticalMover : InfinityLinearMover
{
    [SerializeField] private bool MoveOnUp = true;
    private float maxY;
    private float minY;
    private bool StopCoroutine = false;
    
    void Start()
    {
        _moveObjects = new List<GameObject>();
        maxY = transform.position.y + _objectCount / 2f * _objectDistance;
        minY = transform.position.y - _objectCount / 2f * _objectDistance;
        Vector2 firstObjectPosition = new Vector2(transform.position.x, maxY);

        for(int i = _objectCount; i > 0; i--)
        {
            GameObject item = Instantiate(_moveObjectPref, transform);
            item.transform.position = firstObjectPosition - Vector2.up * i * _objectDistance;
            _moveObjects.Add(item);
        }
        if (MoveOnUp)
        {
            StartCoroutine(VerticalMoveUpObjects());
        }
        else
        {
            StartCoroutine(VerticalMoveDownObjects());
        }
    }

    public void StopMoving()
    {
        StopCoroutine = true;
    }

    public void MoveUp()
    {
        StartCoroutine(VerticalMoveUpObjects());
    }

    public void MoveDown()
    {
        StartCoroutine(VerticalMoveDownObjects());
    }
    private IEnumerator VerticalMoveUpObjects()
    {
        while (true)
        {
            if (StopCoroutine)
            {
                StopCoroutine = false;
                yield break;
            }
            foreach(GameObject obj in _moveObjects)
            {
                obj.transform.position += Vector3.up * _speed;
            }
            if(_moveObjects[_moveObjects.Count - 1].transform.position.y > maxY)
            {
                Destroy(_moveObjects[_moveObjects.Count - 1]);
                _moveObjects.RemoveAt(_moveObjects.Count - 1);

                GameObject item = Instantiate(_moveObjectPref, transform);
                Vector2 itemPosition = new Vector2(transform.position.x, minY);
                item.transform.position = itemPosition;
                _moveObjects.Insert(0, item);
            }
            yield return new WaitForFixedUpdate();
        }
    }


    private IEnumerator VerticalMoveDownObjects()
    {
        while (true)
        {
            if (StopCoroutine)
            {
                StopCoroutine = false;
                yield break;
            }
            foreach (GameObject obj in _moveObjects)
            {
                obj.transform.position -= Vector3.up * _speed;
            }
            if (_moveObjects[0].transform.position.y < minY)
            {
                Destroy(_moveObjects[0]);
                _moveObjects.RemoveAt(0);

                GameObject item = Instantiate(_moveObjectPref, transform);
                Vector2 itemPosition = new Vector2(transform.position.x, maxY);
                item.transform.position = itemPosition;
                _moveObjects.Insert(_objectCount - 1, item);
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
