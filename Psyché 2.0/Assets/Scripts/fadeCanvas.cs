using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fadeCanvas : MonoBehaviour
{

    //public GameObject fadeScreen;
    //public Material material;
    public static float fadeSpeed;
    public static float alpha = 0.0f;
    private Image fader;
    private float saveTime;
    public GameObject image;
    float count = 0;
    float maxCount = 12;
   // ?  bool positive = true;
    bool delay = false;

    void Start()
    {
        fader = image.GetComponent<Image>();
        Color col1 = fader.color;
        col1.a = alpha;
        fader.color = col1;
    }

    void Update()
    {

        Debug.Log("fade alpha:" + alpha);
        Color col = fader.color;

        if (GameManager.Instance.isFading)
        {
            
            if(alpha == 0)
            {
                saveTime = count-1;
            }
            if (alpha >= 1 )
            {
                delay = true;
                fadeSpeed *= -1;
                saveTime = count-1;
                count = 0;
                alpha = 0.99f;
            }

            if (delay)
            {
                if (count >= maxCount) delay = false;
                alpha = 0.99f;
                saveTime = count - 1;
            }

            if(!delay)
            {
                alpha += fadeSpeed * (count - saveTime);
            }
            //alpha = Mathf.Clamp01(alpha);
            //Debug.Log("fade alpha:" + alpha);
            col.a = alpha;
            fader.color = col;

        }

        if(GameManager.Instance.isFading && alpha <= 0)
        {
            alpha = 0;
            GameManager.Instance.isFading = false;
            fadeSpeed *= -1;
            count = 0;
        }
    }

    private void FixedUpdate()
    {
        count ++;

        //if (alpha > 1) alpha = 1;
    }
}


