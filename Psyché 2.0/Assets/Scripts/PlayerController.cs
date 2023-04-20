using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
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
        CheckIfCanJump();
        CheckIfWallsliding();
        CheckJump();
    }

    private void FixedUpdate()
    {
      
    

         ApplyMovement();
        CheckSurrondings();
    }

    private void CheckIfWallsliding()
    {
        if(isTouchingWall && movementInputDirection == facingDirection && rb.velocity.y <0) 
        {

            IsWallSliding = true;
        }

        else
        {

            IsWallSliding = false;
        }
    }
    private void CheckSurrondings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
    }

    private void CheckIfCanJump()
    {
        if(isGrounded && rb.velocity.y <= 0.01f) 
        {
            amountOfJumpsLeft = amountOfJumps;
        }

        if (isTouchingWall)
        {
            canWallJump = true;
        }

      if(amountOfJumpsLeft <= 0)
        {
            canNormalJump = false;
        }

        else
        {
            canNormalJump = true;
        }
     
    }
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
        }

        if (checkjumpMulti && !Input.GetButton("Jump"))
        {
            checkjumpMulti = false;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * variableJumpHeightMulti);
        }
    }

    private void CheckJump()
    {
        if(jumpTimer > 0)
        {
            //Walljump
            if(!isGrounded && isTouchingWall && movementInputDirection != 0 && movementInputDirection != -facingDirection)
            {
                WallJump();
            }

            else if (isGrounded)
            {
                NormalJump();
            }
        }
       if(isAttemptinToJump)
        {
            jumpTimer -= Time.deltaTime;
        }
    }

    private void NormalJump()
    {
        if (canNormalJump && !IsWallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            amountOfJumpsLeft--;
            jumpTimer = 0;
            isAttemptinToJump = false;
            checkjumpMulti = true;
        }


    }

    private void WallJump()
    {
         if (canWallJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0.0f);
            IsWallSliding = false;
            amountOfJumpsLeft = amountOfJumps;
            amountOfJumpsLeft--;
            Vector2 forceToAdd = new Vector2(wallJumpForce * wallJumpDirection.x * movementInputDirection, wallJumpForce * wallJumpDirection.y);
            rb.AddForce(forceToAdd, ForceMode2D.Impulse);
            jumpTimer = 0;
            isAttemptinToJump = false;
            checkjumpMulti = true;
            turnTimer = 0;
            canMove = true;
            canFlip = true;
        }
    }
    private void ApplyMovement()
    {
  

        if (!isGrounded && !IsWallSliding && movementInputDirection == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x * airDragMultiplier, rb.velocity.y);
        }


       else if(canMove)
        {
       
            rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y);
        }
       

        

        if (IsWallSliding)
        {
            if(rb.velocity.y < -wallSlideSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
            }
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
