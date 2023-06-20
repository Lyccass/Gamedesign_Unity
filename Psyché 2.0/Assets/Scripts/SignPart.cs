using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignPart : MonoBehaviour
{
    // Start is called before the first frame update
    ParticleSystem p;
    private void OnEnable()
    {
        if (p != null)
        {
          p.Play();
        }
      
    }

    void Start()
    {
        p = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
