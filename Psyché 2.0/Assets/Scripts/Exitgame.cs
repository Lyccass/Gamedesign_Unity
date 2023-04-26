using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Exitgame : MonoBehaviour
{
	public Button exit;

	void Start()
	{
		Button btn = exit.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick()
	{
		Debug.Log("You have clicked the button!");
		Application.Quit();

	}
}