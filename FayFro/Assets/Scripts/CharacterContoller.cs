using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;



public class CharacterContoller : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;

    //Move
    public float maxSpeed = 10f;
    private bool isFacingRight = true;

    //Jump
    public Transform groundCheck;
    public LayerMask whatIsGround;
    private bool isGrounded = false;
    private const float groundRadius = 0.2f;

    //Jump from wall
    public LayerMask whatIsWall;
    public Transform wallCheckLeft;//rotating, when flip
    public Transform wallCheckRight;
    private const float wallRadius = 0.2f;

    //Impulses
    private const int upImpulse = 600;
    private const int horizontalImpulse = 3000;
    private const int downImpulse = 450;

    //Dash
    private const float dashCheckDistance = 3.0f;
    private const float dashDistance = 3.0f;

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

        if (hitColliders == null || hitColliders.transform.tag == "Chest" || hitColliders.transform.tag == "Secret")
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

        //Move
        float move = Input.GetAxis("Horizontal");

        bool canMove = true;
        if (move > 0 && Physics2D.OverlapCircle(wallCheckRight.position, wallRadius, whatIsWall) && transform.localScale.x == localScaleX)
            canMove = false;
        if(move < 0 && Physics2D.OverlapCircle(wallCheckRight.position, wallRadius, whatIsWall) && transform.localScale.x == -localScaleX)
            canMove = false;

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
        //Jump at wall
        if (!isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                bool isWalled =  Physics2D.OverlapCircle(wallCheckLeft.position, wallRadius, whatIsWall) || Physics2D.OverlapCircle(wallCheckRight.position, wallRadius, whatIsWall);
     
                if (isWalled)
                {
                    float move = Input.GetAxisRaw("Horizontal");
                    if(move != 0)
                        rb.AddForce(new Vector2((int)move * horizontalImpulse, upImpulse));
                }

                bool isPlatform = Physics2D.OverlapCircle(wallCheckLeft.position, wallRadius, whatIsGround) || Physics2D.OverlapCircle(wallCheckRight.position, wallRadius, whatIsGround);

                if (isPlatform)
                {
                    rb.AddForce(new Vector2(0, upImpulse));
                }
            }
        }//Simple jump
        else if (Input.GetKeyDown(KeyCode.C))
        {
            
            anim.SetBool("isGround", false);
            
            rb.AddForce(new Vector2(0, upImpulse));
        }
        
        if (Input.GetKeyDown(KeyCode.DownArrow) && !isGrounded)
        {
            rb.AddForce(new Vector2(0, -1 * downImpulse));
        }

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
