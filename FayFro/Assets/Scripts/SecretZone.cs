using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class SecretZone : MonoBehaviour
{
    private Tilemap _tilemap;

    private void Start()
    {
        _tilemap = GetComponent<Tilemap>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<CharacterController2D>() != null)
        {
            GetComponent<TilemapCollider2D>().enabled = false;
            StartCoroutine(ChangeVisibility());
        }
    }

    private IEnumerator ChangeVisibility()
    {
        float r = _tilemap.color.r;
        float g = _tilemap.color.g;
        float b = _tilemap.color.b;

        for (float i = 1; i != 0; i -= 0.025f)
        {
            _tilemap.color = new Color(r, g, b, i);
            yield return null;
        }
    }

}
