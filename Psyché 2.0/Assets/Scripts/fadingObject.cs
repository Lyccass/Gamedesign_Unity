using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// To be used with fake objects that don't have colliders.
// Could be decoration elements such as skulls etc.
public class fadingObject : MonoBehaviour
{
    [SerializeField] private float lowerThreshold = 20f;
    [SerializeField] private float upperThreshold = 40f;
    private Renderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        renderer = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float thresholdDelta = upperThreshold - lowerThreshold;
        float lowerOverflow = GameManager.Instance.Insanity - lowerThreshold;
        //If lowerThreshold is reached
        Color newColor = renderer.material.color;

        if (lowerOverflow > 0)
        {
            float progress = lowerOverflow / thresholdDelta;
            newColor.a = progress;

            Debug.Log("Show with a:" + progress + " lowerOver" + lowerOverflow);

        }
        else
        {
            newColor.a = 0f;
        }
        // Progress = lowerOverFlow/delta = albedo!
        
       renderer.material.color = newColor;

    }
}
