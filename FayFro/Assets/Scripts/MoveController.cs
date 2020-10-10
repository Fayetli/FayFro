using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    [SerializeField] private GameObject _object;
    [SerializeField] private float _moveStep;
    public void MoveToward(Transform target)
    {
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, _object.transform.position.z);
        StartCoroutine(MovingToward(targetPosition));
    }

    private IEnumerator MovingToward(Vector3 targetPosition)
    {
        while(_object.transform.position != targetPosition)
        {
            _object.transform.position = Vector2.MoveTowards(_object.transform.position, targetPosition, _moveStep);
            yield return null;
        }
    }
}
