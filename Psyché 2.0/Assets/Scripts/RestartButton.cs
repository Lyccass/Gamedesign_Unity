using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RestartButton : MonoBehaviour
{
    // Start is called before the first frame update

    public Button button;
    void Start()
    {
        //button.onClick.AddListener(restart);
      Button b =   gameObject.GetComponent<Button>();
        b.onClick.AddListener(restart);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void restart()
    {
        Debug.Log("Restart clicked");
        GameManager.Instance.restartGame();
    }
}
