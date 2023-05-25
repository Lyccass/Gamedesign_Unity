using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerLight : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 standardSize;
    Vector3 currentGoalSize;
    /***
     * The maximum percentage the light radius exceeds the standard size
     */
    public float maxDeviationPercentage = 0.3f;
    public float speed = 0.001f;

    private bool grow = true;
    void Start()
    {
        standardSize = gameObject.transform.localScale;

        // Grow = true
        currentGoalSize = standardSize * (1 + Random.Range(0f, maxDeviationPercentage));

    }

    // Update is called once per frame
    void Update()
    {
        if (grow)
        {
            gameObject.transform.localScale += Vector3.one *speed ;
            
            // If current scale exceeds goalsize
            if(gameObject.transform.localScale.x >= currentGoalSize.x)
            {
              
                generateNextRadius();
            }

        }
        else
        {
            gameObject.transform.localScale -= Vector3.one * speed;

            // If current scale exceeds goalsize
            if (gameObject.transform.localScale.x <= currentGoalSize.x)
            {

                generateNextRadius();
            }
        }


        


    }

    void generateNextRadius()
    {

        grow = !grow;
        if (grow)
        {
            currentGoalSize = standardSize * (1 + Random.Range(0f, maxDeviationPercentage));
        }
        else
        {
            currentGoalSize = standardSize * (1 - Random.Range(0f, maxDeviationPercentage));
        }

    }
}
