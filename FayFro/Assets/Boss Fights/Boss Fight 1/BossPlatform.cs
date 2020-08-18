using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPlatform : MonoBehaviour
{
    [SerializeField] private DefaultBoss _controller;
    [SerializeField] private Sprite[] _sprites;

    private int i = 0;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Box>() != null)
        {
            _controller.TakeDamage();
            if(i != _sprites.Length - 1)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = _sprites[i];
                i++;
            }
            collision.gameObject.GetComponent<ResetObject>().Destroy();
        }
    }
}
