using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPlayerMover : MonoBehaviour
{
    [SerializeField] private Transform _boxOverlap = null;
    [SerializeField] private LayerMask _whatIsBox = 0;

    private GameObject _box;
    public bool IsBox = false;
    private CharacterController2D _controller;


    private void Awake()
    {
        _controller = gameObject.GetComponent<CharacterController2D>();
    }

    public void StopDrag()
    {
        StopCoroutine(CheckingDrag());
        RevertBox();
    }

    public void ContinueDrag()
    {
        StartCoroutine(CheckingDrag());
    }

    private IEnumerator CheckingDrag()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Vector2 postion = new Vector2(_boxOverlap.transform.position.x, _boxOverlap.transform.position.y);
                Collider2D hit = Physics2D.OverlapCircle(postion, 0.05f, _whatIsBox);
                if (hit != null && hit.gameObject.GetComponent<Box>() != null)
                {
                    _box = hit.gameObject;
                    _box.transform.parent = gameObject.transform;
                    _box.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                    IsBox = true;
                }

            }
            else if (Input.GetKey(KeyCode.Z))
            {


                if (_box != null)
                {
                    Vector2 postion = new Vector2(_boxOverlap.transform.position.x, _boxOverlap.transform.position.y);
                    Collider2D hit = Physics2D.OverlapCircle(postion, 0.25f, _whatIsBox);

                    if (hit == null || hit.gameObject.GetComponent<Box>() == null || _controller.IsGrounded() == false)
                    {
                        RevertBox();
                    }

                }
            }
            else if (Input.GetKeyUp(KeyCode.Z))
            {
                if (_box != null)
                {
                    RevertBox();
                }
            }



            yield return null;
        }
    }

    private void RevertBox()
    {
        if (_box != null)
        {
            _box.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            _box.GetComponent<Box>().SetStartParent();
            _box = null;
            IsBox = false;
        }
    }



}
