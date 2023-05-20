using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneButton : MonoBehaviour
{


    public string sceneToLoad;

    public void LoadScene()
    {
        PauseMenu.isPaused = true;
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneToLoad);
    }
}