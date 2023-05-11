using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;

public class InsanityVisuals : MonoBehaviour
{
    // Start is called before the first frame update

    private PostProcessVolume p;

    public float vignetteStart = 0.3f;
    public float vignetteEnd = 0.8f;
    public float vignetteMax = 0.5f;
    private float vignetteRange;

    public float abberationStart = 0.4f;
    public float abberationEnd = 1f;
    public float abberationMax = 1f;
    private float abberationRange;

    public float distortionStart = 0.4f;
    public float distortionEnd = 1f;
    public float distortionMax = -0.5f;
    private float distortionRange;

    public float grainStart = 0.2f;
    public float grainEnd = 1f;
    public float grainMax = 0.5f;
    private float grainRange;



    private float insanityPercentage;

    void Start()
    {
        p = gameObject.GetComponent<PostProcessVolume>();


        // Calculate effect ranges
        vignetteRange = vignetteEnd - vignetteStart;
        abberationRange = abberationEnd - abberationStart;
        distortionRange = distortionEnd - distortionStart;
        grainRange = grainEnd - grainStart;

    }

    // Update is called once per frame
    void Update()
    {
        insanityPercentage = GameManager.Instance.Insanity / 100;


        UpdateEffect(out p.profile.GetSetting<Vignette>().intensity.value, vignetteStart, vignetteMax, vignetteRange);
        UpdateEffect(out p.profile.GetSetting<ChromaticAberration>().intensity.value, abberationStart, abberationMax, abberationRange);

        // Multiply by 100 manually
      //  float distortionValue;
       // UpdateEffect(out distortionValue, distortionStart, distortionMax, distortionRange);
       // p.profile.GetSetting<LensDistortion>().intensity.value = distortionValue * 100;

        UpdateEffect(out p.profile.GetSetting<LensDistortion>().intensity.value, distortionStart, distortionMax, distortionRange);

        UpdateEffect(out p.profile.GetSetting<Grain>().intensity.value, grainStart, grainMax, grainRange);

    }

    /// <summary>
    /// Updates the current intensity value of a post-processing effect
    /// </summary>
    /// <param name="intensityValue">The output field</param>
    /// <param name="effectStart">The lower threshold of insanity percentage</param>
    /// <param name="effectMax">The max percentage of intensity for this effect</param>
    /// <param name="effectRange">The difference between lower and upper threshold</param>
    /// <param name="negative">If the output intensity shloud be negative</param>
    void UpdateEffect(out float intensityValue, float effectStart, float effectMax, float effectRange)
    {


        float effectOverflow = insanityPercentage - effectStart;
        float effectValue;
        if (effectOverflow > 0)
        {
            float effectProgress = effectOverflow / effectRange;

            // Cap progress at 1 if max is reached
            if (effectProgress >= 1)
            {
                effectProgress = 1;
            }

            effectValue = effectProgress * effectMax;
        }
        else
        {
            effectValue = 0f;
        }

        intensityValue = effectValue;
    }
}
