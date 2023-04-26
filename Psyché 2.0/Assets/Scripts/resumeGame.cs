using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class resumeGame : MonoBehaviour
{
	public Button resume;

	void Start()
	{
		Button btn = resume.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick()
	{
		Debug.Log("You have clicked the button!");
		PauseMenu.isPaused = !PauseMenu.isPaused;
	}
}