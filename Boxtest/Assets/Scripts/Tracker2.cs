using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracker2 : MonoBehaviour
{
    public Transform trackedObject;
    public float updateSpeed = 3;
    public Vector2 trackingOffset;
    private Vector3 offset;
    
    // Start is called before the first frame update
    void Start()
    {
        offset = (Vector3)trackingOffset;
        offset.z = transform.position.z - trackedObject.position.z;
    }

    // Update is called once per frame
    void LateUpdate()
    {


     


            Vector3 newPosition = trackedObject.position + offset;
           
            
            if (PlayerMovement.isJumping )
            {
               newPosition.y = PlayerMovement.groundY +offset.y;
            }

            // Alte pos, neue Pos, limit
            transform.position = Vector3.MoveTowards(transform.position, newPosition, updateSpeed * Time.deltaTime);

       
        // y nicht verändern, wenn player springt
      


    }
}
