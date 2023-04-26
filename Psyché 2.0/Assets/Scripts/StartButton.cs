using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{


    public string sceneToLoad;

    public void LoadScene()
    {
        PauseMenu.isPaused = true;
        SceneManager.LoadScene(sceneToLoad);
    }
}