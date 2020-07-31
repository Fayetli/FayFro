using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPlayerMover : MonoBehaviour
{
    [SerializeField] private Transform _boxOverlap = null;
    [SerializeField] private LayerMask _whatIsBox = 0;

    private PlayerMovement playerMovement = null;
    private float _boxDistance = 1f;
    private GameObject _box;
    private Rigidbody2D _boxRigidbody;


    private Vector3 _velocity = Vector3.zero;
    private float _movementSmoothing = .01f;
    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Vector2 postion = new Vector2(_boxOverlap.transform.position.x, _boxOverlap.transform.position.y);
            Collider2D hit = Physics2D.OverlapCircle(postion, 0.05f, _whatIsBox);

            if (hit != null)
            {
                if (hit.gameObject.GetComponent<Box>() != null)
                {
                    _box = hit.gameObject;
                    _boxRigidbody = _box.GetComponent<Rigidbody2D>();
                }
            }
        }
        else if (Input.GetKey(KeyCode.Z))
        {


            if (_box != null)
            {
                Vector2 postion = new Vector2(_boxOverlap.transform.position.x, _boxOverlap.transform.position.y);
                Collider2D hit = Physics2D.OverlapCircle(postion, 0.25f, _whatIsBox);

                if (hit == null)
                {
                    _box = null;
                    _boxRigidbody = null;
                }
                else if (hit.gameObject.GetComponent<Box>() == null)
                {
                    _box = null;
                    _boxRigidbody = null;
                }

            }
        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {
            if (_box != null)
            {
                _box = null;
                _boxRigidbody = null;
            }
        }


    }

    private void FixedUpdate()
    {
        if (_box != null)
        {
            Vector3 targetVelocity = new Vector2(playerMovement.GetMove() * 10 * Time.deltaTime, _boxRigidbody.velocity.y);
            _boxRigidbody.velocity = Vector3.SmoothDamp(_boxRigidbody.velocity, targetVelocity, ref _velocity, _movementSmoothing);
        }
    }

    public bool IsBox()
    {
        return (_box != null);
    }
}
