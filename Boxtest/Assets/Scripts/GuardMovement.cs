using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardMovement : MonoBehaviour
{

    Vector3 initialPosition;
    public float leftBorder;
    public float rightBorder;
    Vector3 position;
    public float speed;
    bool moveLeft = true;
    bool moveRight = false;
  //  public GameObject guard;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = gameObject.transform.position;
        position = initialPosition;
    }

    // Update is called once per frame
    void Update()
    {

        if (position.x < leftBorder)
        {

            moveRight = true;
            moveLeft = false;
        }


        if(position.x > rightBorder)
        {
            moveLeft = true;
            moveRight = false;
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
}
