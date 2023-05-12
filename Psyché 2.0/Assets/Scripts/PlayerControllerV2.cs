using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Split into animation/visuals and physical movement/collision
// TODO: quasi abspecken und testen!
public class PlayerControllerV2 : MonoBehaviour
{

    // Der scheiß funzt doch safe nicht..
  /*  private static PlayerControllerV2 instance;

        public static PlayerControllerV2 Instance
        {

             get
              {
                if (instance == null) instance = new PlayerControllerV2();
                return instance;
             }
         }*/

    private float movementInputDirection;
    private bool isFacingRight = true;

    private bool isWalking;
    public bool isGrounded;
    public bool isTouchingWall;

    //sleepOverlay;

  
    public GameObject sleepScreen;
    public GameObject sign;

    // Info
    private bool canMove = true;
    private bool canFlip = true;

    // Dann an den stellen jeweils playercontrollerv2.hidden = true
    public static bool hidden;

    //
    public static bool sleeping;

    private bool ducking = false;

    private Rigidbody2D rb;
    private Animator anim;

    public int amountOfJumps = 1;
    private int facingDirection = 1;

    public float movementSpeed = 8f;
    public float minimumSpeed =  4f;

    public float jumpforce = 16.0f;
    public float minimumJumpforce = 10f;


    public float groundCheckRadius;
    public float wallCheckDistance;
   // public float wallSlideSpeed;
    public float movementForceInAir;
    public float airDragMultiplier = 0.95f;
    public float variableJumpHeightMulti = 0.5f;
    //public float wallHopForce;
    //public float wallJumpForce;
    public float jumpTimerSet = 0.15f;
    public float turntimerSet = 0.1f;
    

    private float timer = 0f;



    public Vector2 wallHopDirection;
    public Vector2 wallJumpDirection;

    public Transform groundCheck;
    public Transform wallCheck;
    public LayerMask whatIsGround;

    public BoxCollider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
      //  amountOfJumpsLeft = amountOfJumps;
      //  wallHopDirection.Normalize();
     //   wallJumpDirection.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Sleeping: " +sleeping + " hiding: " + hidden + "" );

        timer += Time.deltaTime;

        if (timer >= 0.1f)
        {
            // Has to be done here, because GameManager is no MonoBehaviour
            // TODO: Maybe outsource to extra "Insanityupdater" Script or something


            if (!sleeping)
            {
                GameManager.Instance.addInsanity(0.2f);
            }
            else
            {
                GameManager.Instance.decrementInsanity(0.6f);
            }
            
            timer = 0;
        }

        if (hidden)
        {
            gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Hidden";
            isWalking = false;
            sign.SetActive(false);

        }
        else
        {

            gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
            //isWalking = true;

        }
        //TODO: hidden flag entfernen, durch canmove ersetzen! canmove wird dann bei interact mit versteck false/treu gesetzt, oder beim schlafen!
        // Hidden bleibt aber trotzdem erhalten, um wachen-collision auszuschalten, die prüfen dann auf player.instance.hidden

        CheckInput();
        checkMovementDirection();
        //Debug.Log(" walking: " + isWalking);
        //UpdateAnimations();
        //CheckIfCanJump();
        if (GameManager.Instance.restart)
        {
            setBackToCheckpoint();
            sleeping = false;
            hidden = false;
            GameManager.Instance.restart = false;
        }


        if ( GameManager.Instance.IsGameOver)
        {
            sleeping = false;
            hidden = false;
            sleepScreen.SetActive(false);
        }


    }

    private void FixedUpdate()
    {
        UpdateAnimations();
        ApplyMovement();
        CheckSurrondings();

    }

    private void CheckSurrondings()
    {
        // kind of replaces onCollision()
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
    }


    // Flip rotates the model to the other direction
    private void checkMovementDirection()
    {
    
        if(hidden || sleeping)
        {
            // Dont flip ewhen sleeping/hiding
            return;
        }

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
      //  Debug.Log(isWalking);
    }

    private void UpdateAnimations()
    {

        if (!(rb.velocity.x  > minimumSpeed || rb.velocity.x < -minimumSpeed))
        {
            isWalking = false;
        }

        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelo", rb.velocity.y);
   
       // anim.SetBool("isWall", IsWallSliding);
    }

    
    private void CheckInput()
    {
        if (GameManager.Instance.IsGameOver)
        {
            return;
        }

	if(Input.GetKeyDown(KeyCode.L)){
		GameManager.Instance.cheats = !GameManager.Instance.cheats;

	}

          movementInputDirection = Input.GetAxisRaw("Horizontal");

//        Debug.Log(isGrounded);
          if (Input.GetButtonDown("Jump"))
          {
              if(isGrounded )
              {
                  NormalJump();
              }

          }

          //TODO: Provide right button

        if (Input.GetKeyDown(KeyCode.Z) && isGrounded && !hidden ){


            if (sleeping)
            {

                Debug.Log("Not Sleeping!");
                sleeping = false;
                sleepScreen.SetActive(false);

            }
            else
            {
                Debug.Log("Sleeping!");
                sleeping = true;
                sleepScreen.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            duck();
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            unduck();
        }
        
    }
    

    private void hide()
    {

    }

    private void duck()
    {
        // TODO: use animation to transit to ducking sprite

        // half height
        collider.size *= new Vector2(1f, 0.5f);
        // half speed
        movementSpeed *= 0.5f;
    }

    private void unduck()
    {
        // double height
        collider.size *= new Vector2(1f,2f);
        movementSpeed *= 2f;

    }
    private void NormalJump()
    {
        if (sleeping)
        {
            return;
        }

        float currentJumpforce = jumpforce * (1 - (GameManager.Instance.Insanity / 200));

        if(currentJumpforce < minimumJumpforce)
        {
            currentJumpforce = 0;
        }
            rb.velocity = new Vector2(rb.velocity.x, currentJumpforce);
            //amountOfJumpsLeft--;
           // isAttemptinToJump = false;
           // checkjumpMulti = true;

    }
    private void ApplyMovement()
    {

        if (sleeping || hidden)
        {
            // If sleeping / hiding, stop moving by setting current x-velocity to 0
            rb.velocity = new Vector2(0, rb.velocity.y);
            isWalking = false;
            return;
            // nciht return ?
        }
            
        if(!hidden && !sleeping)
        {

            float currentSpeed = movementSpeed * (1-(GameManager.Instance.Insanity / 200));

            if(currentSpeed< minimumSpeed)
            {
                currentSpeed = 0;
            }

            rb.velocity = new Vector2(currentSpeed * movementInputDirection, rb.velocity.y);
        }
    }

    private void Flip()
    {
        
        if (canFlip)
        {
            Debug.Log("Flipped!");
            facingDirection *= -1;
            isFacingRight = !isFacingRight;
            transform.Rotate(0.0f, 180.0f, 0.0f);
        }
        
    }


    // 
    private void setBackToCheckpoint()
    {
        gameObject.transform.position = GameManager.Instance.checkpoint;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
    }

    private void OnTriggerExit2D(Collider2D collision )
    {
        if (collision.gameObject.CompareTag("triggerSign") && !hidden)
        {
            sign.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("triggerSign") && !hidden)
        {
            sign.SetActive(true);
        } 
        else
        {
            sign.SetActive(false);
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("triggerSign") && !hidden)
        {
            sign.SetActive(true);
        }
        if (collision.gameObject.CompareTag("triggerSign") && hidden)
        {
            sign.SetActive(false);
        }

    }
}
