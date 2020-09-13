using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CapsuleCollider2D), typeof(Animator), typeof(BoxPlayerMover))]
public class CharacterController2D : MonoBehaviour
{

    [Header("Settings")]
    [Space]
    [SerializeField] private float _jumpForce = 365f;
    [SerializeField] private LayerMask _whatIsGround = 0;
    [SerializeField] private LayerMask _whatIsTPWall = 0;
    [SerializeField] private bool _facingRight = true;
    const float k_groundedRadius = 0.02f;
    const float k_tpDistance = 2.56f;
    private float WaitToDashTime
    {
        get
        {
            return 0.2f;
        }
    }
    private float MovementSmoothing
    {
        get
        {
            return .01f;
        }
    }
    private bool AirControl
    {
        get
        {
            return true;
        }
    }

    [Header("Require Components")]
    [Space]
    [SerializeField] private Transform _groundCheck = null;                           
    [SerializeField] private Transform _tpRaycaster = null;
    [SerializeField] private GameObject _shield = null;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private GameObject _destroyObject;
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private BoxPlayerMover _boxMover;

    #region AutoSettings
    private Vector3 _velocity = Vector3.zero;
    private bool _haveDoubleJump = false;
    private bool _jumpOnAir = false;
    private bool _grounded;
    #endregion

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _shield.SetActive(false);

        _rigidbody = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        _boxMover = gameObject.GetComponent<BoxPlayerMover>();
    }

    private void FixedUpdate()
    {
        bool wasGrounded = _grounded;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        _grounded = Physics2D.OverlapCircle(_groundCheck.position, k_groundedRadius, _whatIsGround);

        if (_grounded)
        {
            if (!wasGrounded)
            {
                CreateDust();
                OnLandEvent.Invoke();
            }

            if (_haveDoubleJump)
            {
                _jumpOnAir = true;
            }
        }
        _animator.SetBool("Ground", _grounded);
        _animator.SetFloat("vSpeed", _rigidbody.velocity.y);

    }

    public void SetHaveDoubleJumpTrue()
    {
        _haveDoubleJump = true;
    }
    public bool IsGrounded()
    {
        return _grounded;
    }

    private IEnumerator Dash(float x, float y, float direction)
    {
        yield return new WaitForSeconds(WaitToDashTime);

        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        _rigidbody.bodyType = RigidbodyType2D.Dynamic;

        transform.position = new Vector2(x + direction * 0.96f, y);
    }


    private void TryToDash()
    {
        RaycastHit2D hit = Physics2D.Raycast(_tpRaycaster.transform.position, transform.TransformDirection(Vector2.right), k_tpDistance, _whatIsTPWall);

        if (hit.collider != null)
        {
            GameObject anim = Instantiate(_destroyObject);
            anim.transform.position = gameObject.transform.position;
            if (_facingRight == false)
            {
                anim.transform.Rotate(0f, 180f, 0f);
            }

            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            _rigidbody.bodyType = RigidbodyType2D.Static;


            float direction = transform.TransformDirection(Vector3.right).x;

            StartCoroutine(Dash(hit.transform.position.x, transform.position.y, direction));
        }

    }

    private void UseShield(bool useShield)
    {
        if (useShield)
        {
            if (!_shield.activeInHierarchy)
            {
                _shield.SetActive(true);
            }
        }
        else
        {
            if (_shield.activeInHierarchy)
            {
                _shield.SetActive(false);
            }
        }

    }


    public void Move(float move, bool jump, bool dash, bool useShield)
    {
        bool withBox = _boxMover.IsBox;
        if (withBox)
        {
            jump = false;
            move /= 4;
        }

        UseShield(useShield);
        if (useShield)
        {
            move = 0.0f;
            dash = false;
            jump = false;
        }

        if (dash)
        {
            TryToDash();
        }

        //only control the player if grounded or airControl is turned on
        if (_grounded || AirControl)
        {
            if (_grounded && Mathf.Abs(move) > 0)
            {
                //CreateDust();
            }
            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, _rigidbody.velocity.y);
            // And then smoothing it out and applying it to the character
            _rigidbody.velocity = Vector3.SmoothDamp(_rigidbody.velocity, targetVelocity, ref _velocity, MovementSmoothing);

            _animator.SetFloat("Speed", Mathf.Abs(move));

            if (withBox == false)
            {
                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !_facingRight)
                {
                    Flip();
                }
                // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && _facingRight)
                {

                    Flip();
                }
            }
        }
        // If the player should jump...
        if ((_grounded || _jumpOnAir) && jump)
        {
            CreateDust();
            _rigidbody.velocity = new Vector2(0, 0);
            _rigidbody.AddForce(new Vector2(0f, _jumpForce));
            _grounded = false;
        }
    }


    private void Flip()
    {
        _facingRight = !_facingRight;
        transform.Rotate(0f, 180f, 0f);
        if (_grounded)
        {
            CreateDust();
        }
    }

    public void CameraFlip()
    {
        _facingRight = !_facingRight;
    }

    private void CreateDust()
    {
        _particleSystem.Play();
    }
}
