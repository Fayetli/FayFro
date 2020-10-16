using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CapsuleCollider2D), typeof(Animator), typeof(BoxPlayerMover))]
public class CharacterController2D : MonoBehaviour
{
    #region JumpSettings
    [Header("Settings")]
    [Space]
    [SerializeField] private float _jumpForce = 365f;
    [SerializeField] private LayerMask _whatIsGround = 0;
    [SerializeField] private LayerMask _whatIsTPWall = 0;
    [SerializeField] private bool _facingRight = true;
    const float k_groundedRadius = 0.01f;
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
    #endregion

    #region References
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
    #endregion

    #region AutoSettings
    private Vector3 _velocity = Vector3.zero;
    private int _jumpCountOnAir = 0;
    private int _jumpMaxCountOnAir = 0;
    private bool _grounded;
    #endregion

    #region Events
    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;
    #endregion


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

        _grounded = Physics2D.OverlapCircle(_groundCheck.position, k_groundedRadius, _whatIsGround);

        if (_grounded)
        {
            if (!wasGrounded)
            {
                Debug.Log("Wasn`t grounded detected");
                _jumpCountOnAir = _jumpMaxCountOnAir;
                CreateDust();
                OnLandEvent.Invoke();
            }


        }
        _animator.SetBool("Ground", _grounded);
        _animator.SetFloat("vSpeed", _rigidbody.velocity.y);

    }
    public void IncreaseJumpOnAir(int value)
    {
        _jumpCountOnAir += value;
    }
    public void RefreshJump()
    {
        _jumpCountOnAir = _jumpMaxCountOnAir;
    }

    public void SetJumpOnAirMaxCount(int count)
    {
        Debug.Log("Increased Max Jump Count");
        _jumpMaxCountOnAir = count;
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

        if (_grounded || AirControl)
        {
            Vector3 targetVelocity = new Vector2(move * 10f, _rigidbody.velocity.y);
            _rigidbody.velocity = Vector3.SmoothDamp(_rigidbody.velocity, targetVelocity, ref _velocity, MovementSmoothing);

            _animator.SetFloat("Speed", Mathf.Abs(move));

            if (withBox == false)
            {
                if (move > 0 && !_facingRight)
                {
                    Flip();
                }
                else if (move < 0 && _facingRight)
                {

                    Flip();
                }
            }
        }
        if (jump)
        {
            if (_grounded)
            {
                Jump();
            }
            else if (_jumpCountOnAir > 0)
            {
                Jump();
                _jumpCountOnAir--;
            }
        }
    }

    private void Jump()
    {
        CreateDust();
        _rigidbody.velocity = new Vector2(0, 0);
        _rigidbody.AddForce(new Vector2(0f, _jumpForce));
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
