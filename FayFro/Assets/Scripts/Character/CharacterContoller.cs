using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;



public class CharacterContoller : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;

    //Move
    public float maxSpeed = 5f;
    private bool isFacingRight = true;

    //Jump
    public Transform groundCheck;
    public LayerMask whatIsGround;
    private bool isGrounded = false;
    private const float groundRadius = 0.01f;

    //Jump from wall
    public LayerMask _whatIsDontMoveLayer;
    public Transform wallCheckLeft;//rotating, when flip
    public Transform wallCheckRight;

    //Impulses
    public int upImpulse = 300;
    public int downImpulse = 150;

    //Dash
    public float dashCheckDistance = 3.0f;
    public float dashDistance = 3.0f;

    //To Flip
    private float localScaleX;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        localScaleX = transform.localScale.x;
    }
    void Dash(int xSymbol, int ySymbol)
    {
        float x = this.gameObject.transform.position.x;
        float y = this.gameObject.transform.position.y;
        //change this
        Collider2D hitColliders = Physics2D.OverlapCapsule(new Vector2(x + xSymbol * dashCheckDistance, y + ySymbol * dashCheckDistance), new Vector2(0.3f, 0.4f), CapsuleDirection2D.Vertical, 0f);

        if (hitColliders == null || hitColliders.transform.CompareTag("Chest") || hitColliders.transform.CompareTag("Secret") || hitColliders.transform.CompareTag("Ladder"))
        {
            this.transform.position = new Vector2(x + xSymbol * dashDistance, y + ySymbol * dashDistance);
            StatCharacterController.player.AddDash(-1);
            InterfaceManager.PrintDash();
        }

    }
    void FixedUpdate()
    {
        //Jump
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("isGround", isGrounded);
        anim.SetFloat("vSpeed", rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.C) && isGrounded)
        {

            anim.SetBool("isGround", false);

            rb.AddForce(new Vector2(0, upImpulse));
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && !isGrounded)
        {
            rb.AddForce(new Vector2(0, -1 * downImpulse));
        }

        //Move
        float move = Input.GetAxis("Horizontal");

        bool canMove = true;
        Collider2D hitColliders = Physics2D.OverlapBox(wallCheckRight.position, new Vector2(0.01f, 0.16f), 0f, _whatIsDontMoveLayer);
        Debug.Log(hitColliders);
        if (move > 0 && hitColliders != null && transform.localScale.x == localScaleX && !isGrounded)
        {
            canMove = false;
        }
        if (move < 0 && hitColliders != null && transform.localScale.x == -localScaleX && !isGrounded)
        {
            canMove = false;
        }

        if (canMove)
        {
            rb.velocity = new Vector2(move * maxSpeed, rb.velocity.y);

            anim.SetFloat("Speed", Mathf.Abs(move));

            if (move > 0 && !isFacingRight)
                Flip();
            else if (move < 0 && isFacingRight)
                Flip();
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 theScale = transform.localScale;

        theScale.x *= -1;

        transform.localScale = theScale;
    }

    private void Update()
    {

       

        //Dash
        if (isGrounded && StatCharacterController.player.GetDash() != StatCharacterController.player.GetMaxDash())
        {
            StatCharacterController.player.SetDashToMaxDash();
            InterfaceManager.PrintDash();
        }
        if (Input.GetKeyDown(KeyCode.X) && StatCharacterController.player.CanDash())
        {
            Dash((int)Input.GetAxisRaw("Horizontal"), (int)Input.GetAxisRaw("Vertical"));
        }
        
    }
}
