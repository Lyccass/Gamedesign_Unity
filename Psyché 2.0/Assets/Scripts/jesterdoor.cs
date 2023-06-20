using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jesterdoor : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject door;
    public GameObject gitter;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     if(door == null)
        {
            gitter.SetActive(true);
        }   
    }
}
