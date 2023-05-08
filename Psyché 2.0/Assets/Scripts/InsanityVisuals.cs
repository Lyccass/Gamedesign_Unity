using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;

public class InsanityVisuals : MonoBehaviour
{
    // Start is called before the first frame update

    PostProcessVolume p;
    
    void Start()
    {
        p = gameObject.GetComponent<PostProcessVolume>();

        PostProcessEffectSettings s;
        // p.profile.TryGetSettings(out s);

      //  p.profile.GetSetting<Vignette>().intensity.value = GameManager.Instance.Insanity/100;
    }

    // Update is called once per frame
    void Update()
    {
        p.profile.GetSetting<Vignette>().intensity.value = GameManager.Instance.Insanity / 100;
    }
}
