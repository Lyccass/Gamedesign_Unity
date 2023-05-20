using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class insanityManager : MonoBehaviour
{
    private static insanityManager instance = null;
    private static float insanity = 0f;
    private static float timer = 0f;

    public static float Insanity { get => insanity; }

   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Increase Insanity over time
        timer += Time.deltaTime;
        if(timer >=0.25)
        {
            insanity += 0.5f;
            timer = 0;
        }

        Debug.Log("Insanity " + insanity);
    }

    // Decreases the current Insanity level by a given value, but never below 0.
    // Can be used while sleeping for a specified amount of time.
    public void decreaseInsanity(float value)
    {
        insanity -= value;
        if (insanity < 0)
        {
            insanity = 0;
        }
    }

}
