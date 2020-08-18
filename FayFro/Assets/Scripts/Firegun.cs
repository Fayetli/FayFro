using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firegun : MonoBehaviour
{
    [SerializeField] private GameObject _firePref;
    [SerializeField] private float _timeBetweenFire;

    private void Start()
    {
        StartCoroutine(Fire());
    }


    private bool _stopFire = false;

    private IEnumerator StopFire(float time)
    {
        yield return new WaitForSeconds(time);
        _stopFire = true;
    }

    private IEnumerator Fire()
    {
        int sortingLayer = 1;

        while (true)
        {
            StartCoroutine(StopFire(3.0f));
            while (_stopFire == false)
            {
                int randomCount = Random.Range(1, 3);

                while (randomCount != 0)
                {
                    float randomAddY = Random.Range(-0.10f, 0.10f);
                    GameObject fire = Instantiate(_firePref, gameObject.transform);
                    float x = gameObject.transform.position.x;
                    float y = gameObject.transform.position.y + randomAddY;
                    fire.transform.position = new Vector2(x, y);
                    if (Random.Range(0, 2) == 1)
                    {
                        fire.transform.localScale = new Vector2(fire.transform.localScale.x, fire.transform.localScale.y * -1);
                    }
                    fire.GetComponent<SpriteRenderer>().sortingOrder = sortingLayer;
                    sortingLayer += 1;
                    randomCount -= 1;
                }
                yield return new WaitForSeconds(_timeBetweenFire);
            }
            _stopFire = false;
            yield return new WaitForSeconds(4.0f);
        }
    }
}
