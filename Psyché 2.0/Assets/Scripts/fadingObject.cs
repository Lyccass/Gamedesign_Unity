using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// To be used with fake objects that don't have colliders.
// Could be decoration elements such as skulls etc.
public class fadingObject : MonoBehaviour
{
    [SerializeField] private float lowerThreshold = 20f;
    [SerializeField] private float upperThreshold = 40f;
    [SerializeField] private float jitterIntensity = 2f;
    [SerializeField] private int jitterfrequency = 1;
    [SerializeField] private bool jitter = false;

    private int jitterCounter = 0;
    private Renderer objectrenderer;
    

    // Start is called before the first frame update
    void Start()
    {
        objectrenderer = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float thresholdDelta = upperThreshold - lowerThreshold;
        float lowerOverflow = GameManager.Instance.Insanity - lowerThreshold;
        //If lowerThreshold is reached
        Color newColor = objectrenderer.material.color;

        if (lowerOverflow > 0)
        {
            float progress = lowerOverflow / thresholdDelta;
            newColor.a = progress;

        //    Debug.Log("Show with a:" + progress + " lowerOver" + lowerOverflow);

        }
        else
        {
            newColor.a = 0f;
        }

        if (jitter)
        {
            jitterCounter++;
            if(jitterCounter >= jitterfrequency)
            {
                jitterCounter = 0;

              
                Vector3 shift = new Vector3(Random.Range(-1f,1f), Random.Range(-1f,1f), 0) * jitterIntensity;
                Vector3 newPos = gameObject.transform.position = gameObject.transform.position + shift;
          
            }
            
          
        }
        // Progress = lowerOverFlow/delta = albedo!
        
       objectrenderer.material.color = newColor;

    }
}
