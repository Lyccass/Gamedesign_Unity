using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Split into animation/visuals and physical movement/collision
// TODO: quasi abspecken und testen!
public class PlayerControllerV2 : MonoBehaviour
{

    private float movementInputDirection;
    private float jumpTimer;
    private float turnTimer;

    private int amountOfJumpsLeft;
    private bool isFacingRight = true;

    private bool isWalking;
    public bool isGrounded;
    public bool isTouchingWall;
    private bool canNormalJump;
    private bool canWallJump;
    private bool IsWallSliding;
    private bool isAttemptinToJump;
    private bool checkjumpMulti;
    private bool canMove;
    private bool canFlip;
   public static bool hidden;


    private Rigidbody2D rb;
    private Animator anim;

    public int amountOfJumps = 1;
    private int facingDirection = 1;

    public float movementSpeed = 8f;
    public float jumpforce = 16.0f;
    public float groundCheckRadius;
    public float wallCheckDistance;
    public float wallSlideSpeed;
    public float movementForceInAir;
    public float airDragMultiplier = 0.95f;
    public float variableJumpHeightMulti = 0.5f;
    public float wallHopForce;
    public float wallJumpForce;
    public float jumpTimerSet = 0.15f;
    public float turntimerSet = 0.1f;

    private float timer = 0f;



    public Vector2 wallHopDirection;
    public Vector2 wallJumpDirection;

    public Transform groundCheck;
    public Transform wallCheck;
    public LayerMask whatIsGround;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        amountOfJumpsLeft = amountOfJumps;
        wallHopDirection.Normalize();
        wallJumpDirection.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 1)
        {
            GameManager.Instance.addInsanity(2);
            timer = 0;
        }


        //TODO: hidden flag entfernen, durch canmove ersetzen! canmove wird dann bei interact mit versteck false/treu gesetzt, oder beim schlafen!

        if (hidden)
        {
            movementSpeed = 0f;
            return;
        }
        else
        {
            movementSpeed = 8f;
        }


        CheckInput();
        checkMovementDirection();
        UpdateAnimations();
        //CheckIfCanJump();
    }

    private void FixedUpdate()
    {
      
    

         ApplyMovement();
        CheckSurrondings();
    }

    private void CheckSurrondings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
    }

    private void CheckIfCanJump()
    {
        /*
        if(isGrounded && rb.velocity.y <= 0.01f) 
        {
            amountOfJumpsLeft = amountOfJumps;
        }



      if(amountOfJumpsLeft <= 0)
        {
            canNormalJump = false;
        }

        else
        {
            canNormalJump = true;
        }
     */
    }

    // Flip rotates the model to the other direction
    private void checkMovementDirection()
    {
        if(isFacingRight && movementInputDirection < 0)
        {
            Flip();
        }
        else if(!isFacingRight && movementInputDirection > 0)
        {
            Flip();
        }

        if(rb.velocity.x != 0)
        {
            isWalking = true;
        }

        else
        {
            isWalking = false;
        }
    }

    private void UpdateAnimations()
    {
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelo", rb.velocity.y);
        anim.SetBool("isWall", IsWallSliding);
    }
    private void CheckInput()
    {
        movementInputDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            if(isGrounded || (amountOfJumpsLeft > 0 && !isTouchingWall))
            {
                NormalJump();
            }

            else
            {
                jumpTimer = jumpTimerSet;
                isAttemptinToJump = true;
            }
        }

        if(Input.GetButtonDown("Horizontal") && isTouchingWall)
        {
            if(!isGrounded && movementInputDirection != facingDirection)
            {
                canMove = false;
                canFlip = false;

                turnTimer = turntimerSet;
            }
        }
/*
        if (!canMove)
        {

            canFlip = true;
            canMove = true;

            turnTimer = Time.deltaTime;

            if(turnTimer <=0)
            {
                canFlip = true;
                canMove = true;
            }
        }*/
    }



    private void NormalJump()
    {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            //amountOfJumpsLeft--;
            jumpTimer = 0;
           // isAttemptinToJump = false;
           // checkjumpMulti = true;
        


    }
    private void ApplyMovement()
    {
  
        if(canMove)
        {
       
            rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y);
        }
    }

    private void Flip()
    {
        if (!IsWallSliding  && canFlip)
        {
            facingDirection *= -1;
            isFacingRight = !isFacingRight;
            transform.Rotate(0.0f, 180.0f, 0.0f);
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
    }
}
