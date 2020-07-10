using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMover : MonoBehaviour
{
    [SerializeField] private GameObject _platformPref;

    [SerializeField] private float _radius;

    [SerializeField] private float _angleSpeed;

    [SerializeField] private int _platformCount;

    [SerializeField] private bool Reversed = false;

    private GameObject[] _moveObjects;

    const float k_pi = 3.1415f;

    private int _reversedKoef = 1;

    private float[] _angles;

    private void Start()
    {
        if (Reversed)
        {
            _reversedKoef = -1;
        }

        _moveObjects = new GameObject[_platformCount];

        for(int i = 0; i < _platformCount; i++)
        {
            _moveObjects[i] = Instantiate(_platformPref, this.transform);
        }

        float angle = 360 / _moveObjects.Length;
        _angles = new float[_moveObjects.Length];
        for (int i = 0; i < _moveObjects.Length; i++)
        {
            float objectAngle = i * angle;
            float x = _radius * Mathf.Cos(objectAngle * k_pi / 180);
            float y = _radius * Mathf.Sin(objectAngle * k_pi / 180);
            _moveObjects[i].transform.localPosition = new Vector2(x, y);
            _angles[i] = objectAngle;
        }

        StartCoroutine(MoveObjects());
    }

    private IEnumerator MoveObjects()
    {
        while (true)
        {
            for (int i = 0; i < _moveObjects.Length; i++)
            {
                _angles[i] += _angleSpeed * _reversedKoef;
                float x = _radius * Mathf.Cos(_angles[i] * k_pi / 180);
                float y = _radius * Mathf.Sin(_angles[i] * k_pi / 180);
                _moveObjects[i].transform.localPosition = new Vector2(x, y);
            }

            yield return new WaitForFixedUpdate();
        }
    }

}
