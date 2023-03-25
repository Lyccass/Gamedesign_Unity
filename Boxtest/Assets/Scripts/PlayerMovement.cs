using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public static bool isJumping = false;
    public static bool grounded = false;
    public static float groundY = 0f;
    private float horizontal;
    private float speed = 4f;
    private float jumpingPower = 16f;
    private bool delay = true;
    private float timer = 0f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    void Update()
    {

        timer += Time.deltaTime;

        if(timer >= 3)
        {
            GameManager.Instance.addInsanity(0);
            timer = 0;
        }


        horizontal = Input.GetAxisRaw("Horizontal");

        // TODO: Jump um 1 frame delayen! Allerdings wird Jumping direkt gesetzt, somit kann der Tracker das anwenden
        if (Input.GetButtonDown("Jump") && grounded)
        {
            isJumping = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            isJumping = true;
        }

        if (isJumping && transform.position.y < groundY)
        {
            isJumping = false;
        }


    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }


    public void OnCollisionEnter2D(Collision2D collision)

    {
        if (collision.gameObject.tag.Equals("Ground")){
  Debug.Log("Grounded");
            grounded = true;
            isJumping = false;
            groundY = transform.position.y;
        
        }
      
    }

    public void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject.tag.Equals("Ground"))
        {
           Debug.Log("Un-Grounded");
            grounded = false;

        }
        
    }
}
