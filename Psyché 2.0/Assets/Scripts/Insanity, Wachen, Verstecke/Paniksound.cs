using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paniksound : MonoBehaviour
{

    public AudioSource asour;
    public float volumen = insanityManager.Insanity / 100;



    // Update is called once per frame
    void Update()
    {
        soundchanger();
        Debug.Log("volumen" + volumen);
    }

    public void soundchanger()
    {

       asour.GetComponent<AudioSource>().volume = volumen;
        asour.volume = insanityManager.Insanity / 100;
    }

}
