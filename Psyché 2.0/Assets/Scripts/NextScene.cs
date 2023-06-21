using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public string nextSceneName;
    private void OnEnable()
    {
        SceneManager.LoadScene(nextSceneName, LoadSceneMode.Single);
    }
}
