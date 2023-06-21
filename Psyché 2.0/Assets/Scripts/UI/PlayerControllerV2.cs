using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Split into animation/visuals and physical movement/collision
// TODO: quasi abspecken und testen!
public class PlayerControllerV2 : MonoBehaviour
{

    // Der schei� funzt doch safe nicht..
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
    public static bool isGrounded;
    public static bool isStaired = false;
    public bool isTouchingWall;
   


    //sleepOverlay;


    public GameObject sleepScreen;
    // TODO: immer bei sign auch f anzeigen!
  //  public GameObject sign;
    public GameObject f;
    public GameObject z;
    public GameObject warn;
    // Info
    //private bool canMove = true;
    private bool canFlip = true;

    // Dann an den stellen jeweils playercontrollerv2.hidden = true
    public static bool hidden;

    //
    public static bool sleeping;
    // set in guard?
    public static bool warning;
    public static bool innerWarning;

    private bool ducking = false;

    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D coll;

    public int amountOfJumps = 1;
    private int facingDirection = 1;

    public float movementSpeed = 8f;
    public float minimumSpeed = 4f;
    public float insanityMovementThreshold = 20f;

    public float jumpforce = 16.0f;
    public float minimumJumpforce = 4f;


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
    private Vector2 freezePos = new Vector2(-1,-1);

    private float heartbeatStandardDelay =  1f;
    private float heartbeatTimer = 0f;


    public Vector2 wallHopDirection;
    public Vector2 wallJumpDirection;

    public Transform groundCheck;
    public Transform wallCheck;
    public Transform stairCheck;
    public LayerMask whatIsGround;
    public LayerMask whatIsStair;
    public AudioSource heartbeat;

    private GameObject currentStair = null;

    private BoxCollider2D playercollider;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playercollider = GetComponent<BoxCollider2D>();
        warning = false;
        coll = GetComponent<BoxCollider2D>();
       // heartbeat = GetComponent<AudioSource>();
        //  amountOfJumpsLeft = amountOfJumps;
        //  wallHopDirection.Normalize();
        //   wallJumpDirection.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Sleeping: " +sleeping + " hiding: " + hidden + "" );

        timer += Time.deltaTime;

        if (timer >= 0.1f)
        {
            // Has to be done here, because GameManager is no MonoBehaviour
            // TODO: Maybe outsource to extra "Insanityupdater" Script or something


            if (!sleeping)
            {
                GameManager.Instance.addInsanity(0.1f);
                if (GameManager.Instance.Insanity > 65)
                {
                    setSignZ(true);
                    setSignF(false);
                }
            }
            else
            {

                setSignZ(false);
                GameManager.Instance.decrementInsanity(1.2f);
            }

            timer = 0;
        }

      

        if (hidden)
        {
            gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Hidden";
            isWalking = false;
            setSignF(false);
            setSignZ(false);
        }
        else
        {

            gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
            //isWalking = true;

        }
        //TODO: hidden flag entfernen, durch canmove ersetzen! canmove wird dann bei interact mit versteck false/treu gesetzt, oder beim schlafen!
        // Hidden bleibt aber trotzdem erhalten, um wachen-collision auszuschalten, die pr�fen dann auf player.instance.hidden

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
            anim.SetBool("isDead", false);
        }


        if (GameManager.Instance.IsGameOver)
        {
            sleeping = false;
            hidden = false;
            sleepScreen.SetActive(false);
            anim.SetBool("isDead", true);
            
        }

        if (warning || innerWarning)
        {
            z.SetActive(false);
           // f.SetActive(false);
            setSignWarn(true);
        }
        else
        {
            // Problem: auch wenn z oder f an, wird hier das sign deaktiviert!
            //  soll ausgehen wenn weder z noch f.
            // soll anbleiben wenn 1 von beiden
            setSignWarn(false);
        }
        ApplyMovement();
        playHeartbeat();
        freezePos = rb.position;
    }

    private void FixedUpdate()
    {
        UpdateAnimations();
        
        
        CheckSurrondings();
      

    }

    private void CheckSurrondings()
    {
        // kind of replaces onCollision()
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
    
      //  Debug.Log("Stasired: " + isStaired);




        // TODO: if rb .velocity = down (und not grounded), staircollider = true;
        // -> wenn normal laufen, passiert nix
    }

    // Flip rotates the model to the other direction
    private void checkMovementDirection()
    {

        if (hidden || sleeping)
        {
            // Dont flip ewhen sleeping/hiding
            return;
        }

        if (isFacingRight && movementInputDirection < 0)
        {
            Flip();
        }
        else if (!isFacingRight && movementInputDirection > 0)
        {
            Flip();
        }

        /*
              if(rb.velocity.x != 0)
              {
                  isWalking = true;
              }

              else
              {
                  isWalking = false;
              }

              */
        //  Debug.Log(isWalking);
    }

    private void UpdateAnimations()
    {

        if (!(rb.velocity.x > minimumSpeed || rb.velocity.x < -minimumSpeed))
        {
            // isWalking = false;
        }




        if (movementInputDirection != 0)
        {
            isWalking = true;

        }
        else
        {
            isWalking = false;
            // coll.sharedMaterial.friction = 50f;
        }


        anim.SetBool("isWalking", isWalking);
        // }

        
            
        anim.SetBool("isGrounded", isGrounded);
        // Für jumping
        anim.SetFloat("yVelo", rb.velocity.y);
        anim.SetBool("isResting", sleeping);
        anim.SetBool("isDucking", ducking);
        anim.SetBool("isHiding", hidden);
        // anim.SetBool("isWall", IsWallSliding);
    }


    private void CheckInput()
    {
        if (GameManager.Instance.IsGameOver)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            GameManager.Instance.cheats = !GameManager.Instance.cheats;

        }

        movementInputDirection = Input.GetAxis("Horizontal");

   
       

        //        Debug.Log(isGrounded);
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded||isStaired)
            {
                
                NormalJump();
            }

        }

        //TODO: Provide right button

        if (Input.GetKeyDown(KeyCode.Z) && (isGrounded || isStaired) && !hidden)
        {


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

      /*  if (Input.GetKeyDown(KeyCode.S))
        {
            duck();
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            unduck();
        }
      */
    }


    private void hide()
    {

    }

    private void duck()
    {
        // TODO: use animation to transit to ducking sprite
        ducking = true;
        // half height
        // playercollider.size *= new Vector2(1f, 0.5f);
        // half speed
        movementSpeed *= 0.5f;
    }

    private void unduck()
    {
        ducking = false;
        // double height
        //playercollider.size *= new Vector2(1f, 2f);
        movementSpeed *= 2f;

    }
    private void NormalJump()
    {
        if (sleeping)
        {
            return;
        }
      //  isJumping = true;
        // Adjust jump force to insanity
        float currentJumpforce = jumpforce * (1 - (GameManager.Instance.Insanity / 200));

        if (currentJumpforce < minimumJumpforce)
        {
            currentJumpforce = 4;
        }
        rb.velocity = new Vector2(rb.velocity.x, currentJumpforce);
        //amountOfJumpsLeft--;
        // isAttemptinToJump = false;
        // checkjumpMulti = true;

    }
    private void ApplyMovement()
    {
//         Debug.Log("Poops");



        // momento: wenn grad im sprung, lass mal die y-velocity! 
        // wenn y nach oben (sprung), skip den shit!
        // wenn landen ist aber blös!
        if (isStaired && movementInputDirection == 0 && rb.velocity.y <=0 )
        {
                 rb.position = freezePos;
                 rb.velocity = new Vector2(0,0);

            // ?!? evtl passt springen dann so
           
            
            // wenn jumping, d.h. wenn y-velo > 0

            


            Debug.Log("Poops");
           // rb.bodyType = RigidbodyType2D.Kinematic;// = new Vector2(0, 0);
            
           return;
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }

        if (sleeping || hidden || GameManager.Instance.IsGameOver)
        {
            // If sleeping / hiding, stop moving by setting current x-velocity to 0
            rb.velocity = new Vector2(0, rb.velocity.y);
            isWalking = false;
            return;
            // nciht return ?
        }

        if (!hidden && !sleeping)
        {
            // Adjust movementspeed to Insanity

            // TODO: Maybe only above, say, 30 insanity, then decrease 
            float insanityOverflow = GameManager.Instance.Insanity - insanityMovementThreshold;
            float multiplier = 1f;
            if (insanityOverflow > 0)
            {
                multiplier = 1 - (insanityOverflow / (100f - insanityMovementThreshold));
              
                
                if (multiplier < 0.3f)
                {
                    multiplier = 0.3f;
                }

            }

            float currentSpeed = movementSpeed * multiplier;



            // TODO: Look into further
            //   if(currentSpeed< minimumSpeed)
            //   {
            //       currentSpeed = minimumSpeed;
            //   }
            


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
            // Rotate f and z by 360 in total
            f.transform.Rotate(0.0f, 180.0f, 0.0f);
            z.transform.Rotate(0.0f, 180.0f, 0.0f);
        }

    }

    private void playHeartbeat()
    {


        heartbeatTimer -= Time.deltaTime;
   
        if (heartbeatTimer <= 0)
        {
            if (GameManager.Instance.Insanity > 20)
            {
                heartbeat.Play();

            }

            // reduce by multiplier, e.g. delay/2 at 100 insanity
            float multiplier = 1 + 0.7f*(GameManager.Instance.Insanity / 100) ;
            float currentheartbeatDelay = heartbeatStandardDelay / multiplier;
           // Debug.Log("Beattimer " + currentheartbeatDelay + "multiplier " + multiplier );
            heartbeatTimer = currentheartbeatDelay;
            
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("triggerSign") && !hidden)
        {
            setSignF(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("triggerSign") && !hidden)
        {
            setSignF(true);
        }
        else
        {
            setSignF(false);
        }

    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("triggerSign") && !hidden)
        {
            setSignF(true);
        }
        if (collision.gameObject.CompareTag("triggerSign") && hidden)
        {
            setSignF(false);
        }

    }

    private void setSignF(bool value)
    {
      //  if (warning) return;

        if (z.activeSelf || sleeping)
        {
            // Z overrides f
            f.SetActive(false);
            return;
        }
        else
          //  sign.SetActive(value);
     if(value && f.activeSelf)
        {
            // no spamming of f active
            return;
        }
            f.SetActive(value);
    }
    private void setSignZ(bool value)
    {
        if (warning) return;
       // sign.SetActive(value);
        z.SetActive(value);
    }
    private void setSignWarn(bool value)
    {

        warn.SetActive(value);
        //sign.SetActive(value);

        if (f.activeSelf || z.activeSelf)
        {
          //   sign.SetActive(true);
        }
       

    }
}
