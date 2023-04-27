using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardMovement : MonoBehaviour
{

    Vector3 initialPosition;

    /// <summary>
    /// The range the guard is allowed to walk!
    /// </summary>
    public float walkRange = 15;
    public LayerMask ground;
    public float distanceToWall;

    Vector3 position;
    public float speed;
    bool moveLeft = true;
    bool moveRight = false;

    private float leftBorder;
    private float rightBorder;
    private bool isTouchingWall = false;
    private Vector2 direction = Vector2.left;

    private BoxCollider2D collider;
    //  public GameObject guard;


    // Start is called before the first frame update
    void Start()
    {
        initialPosition = gameObject.transform.position;
        position = initialPosition;
        leftBorder = initialPosition.x - walkRange;
        rightBorder = initialPosition.x + walkRange;
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        checkWalls();

        // Turn on touching wall
        if (isTouchingWall)
        {
            Debug.Log("Guard turned at wall!");
            moveLeft = !moveLeft;
            moveRight = !moveRight;
            
            if(direction == Vector2.left)
            {
                direction = Vector2.right;
            }
            else
            {
                direction = Vector2.left;
            }
            isTouchingWall = false;
        }

        if (position.x < leftBorder)
        {

            moveRight = true;
            moveLeft = false;
            direction = Vector2.right;

        }


        if(position.x > rightBorder)
        {
            moveLeft = true;
            moveRight = false;
            direction = Vector2.left;
        }

        if (moveRight)
        {
            position.x += speed * Time.deltaTime;
        }



        if (moveLeft)
        {
            position.x -= speed * Time.deltaTime;
        }

        gameObject.transform.position = position;
    }


    private void checkWalls()
    {


        isTouchingWall = Physics2D.Raycast(position, direction, distanceToWall, ground);

     }


}
