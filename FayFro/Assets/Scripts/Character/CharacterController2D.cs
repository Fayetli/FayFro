using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CapsuleCollider2D), typeof(Animator), typeof(BoxPlayerMover))]
public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 365f;                          // Amount of force added when the player jumps.
    private float m_MovementSmoothing = .01f;  // How much to smooth out the movement
    private bool m_AirControl = true;                         // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround = 0;                          // A mask determining what is ground to the character
    [SerializeField] private LayerMask _whatIsTPWall = 0;
    [SerializeField] private Transform m_GroundCheck = null;                           // A position marking where to check if the player is grounded.
                                                                                       //[SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
    [SerializeField] private Transform _tpRaycaster = null;
    [SerializeField] private GameObject _shield = null;

    private Animator animator;
    [SerializeField] private GameObject ch_destroy_on_tp;

    const float k_GroundedRadius = 0.02f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded;            // Whether or not the player is grounded.
                                        //const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
    private Rigidbody2D m_Rigidbody2D;
    [SerializeField] private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;
    const float _tpDistance = 2.56f;
    private BoxPlayerMover _boxMover;
    private bool _haveDoubleJump = false;
    private bool _jumpOnAir = false;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    private void Start()
    {
        animator = GetComponent<Animator>();
        _shield.SetActive(false);
        _boxMover = gameObject.GetComponent<BoxPlayerMover>();
    }

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        m_Grounded = Physics2D.OverlapCircle(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);

        if (m_Grounded)
        {
            if (!wasGrounded)
                OnLandEvent.Invoke();

            if (_haveDoubleJump)
            {
                _jumpOnAir = true;
            }
        }
        animator.SetBool("Ground", m_Grounded);
        animator.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);

    }

    public void SetHaveDoubleJumpTrue()
    {
        _haveDoubleJump = true;
    }
    public bool IsGrounded()
    {
        return m_Grounded;
    }

    private float waitToDashTime = 0.2f;
    private IEnumerator Dash(float x, float y, float direction)
    {
        yield return new WaitForSeconds(waitToDashTime);

        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        m_Rigidbody2D.bodyType = RigidbodyType2D.Dynamic;

        transform.position = new Vector2(x + direction * 0.96f, y);
    }


    private void TryToDash()
    {
        RaycastHit2D hit = Physics2D.Raycast(_tpRaycaster.transform.position, transform.TransformDirection(Vector2.right), _tpDistance, _whatIsTPWall);

        if (hit.collider != null)
        {
            GameObject anim = Instantiate(ch_destroy_on_tp);
            anim.transform.position = gameObject.transform.position;
            if (m_FacingRight == false)
            {
                anim.transform.Rotate(0f, 180f, 0f);
            }

            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            m_Rigidbody2D.bodyType = RigidbodyType2D.Static;


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
        if (m_Grounded || m_AirControl)
        {
            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

            animator.SetFloat("Speed", Mathf.Abs(move));

            if (withBox == false)
            {
                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !m_FacingRight)
                {
                    Flip();
                }
                // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight)
                {

                    Flip();
                }
            }
        }
        // If the player should jump...
        if (m_Grounded && jump)
        {
            // Add a vertical force to the player.
            m_Grounded = false;
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            
        }
        else if(jump && _jumpOnAir)
        {
            m_Rigidbody2D.velocity = new Vector2(0, 0);
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            _jumpOnAir = false;
        }
    }


    private void Flip()
    {
        m_FacingRight = !m_FacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    public void CameraFlip()
    {
        m_FacingRight = !m_FacingRight;
    }
}
