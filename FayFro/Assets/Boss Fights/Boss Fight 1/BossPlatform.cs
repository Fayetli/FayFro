using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPlatform : MonoBehaviour
{
    [SerializeField] private DefaultBoss _controller;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Box>() != null)
        {
            _controller.TakeDamage();
            Debug.Log("Boss attacked! Hp: " + _controller.GetHP());
        }
    }
}
