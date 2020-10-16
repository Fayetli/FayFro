using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
public class BonusJump : MonoBehaviour
{
    [SerializeField] private float _waitTime = 3.0f;

    private SpriteRenderer _sprite;
    private Collider2D _collider;

    private void Start()
    {
        _sprite = gameObject.GetComponent<SpriteRenderer>();
        _collider = gameObject.GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<CharacterController2D>() != null)
        {
            collision.GetComponent<CharacterController2D>().IncreaseJumpOnAir(1);
            StartCoroutine(Disappearing());
        }
    }

    private IEnumerator Disappearing()
    {
        Appear(false);
        yield return new WaitForSeconds(_waitTime);
        Appear(true);
    }

    private void Appear(bool value)
    {
        _sprite.enabled = value;
        _collider.enabled = value;
    }

}
