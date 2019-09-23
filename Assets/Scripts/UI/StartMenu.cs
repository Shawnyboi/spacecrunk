using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
	public void StartMatch()
	{
		SceneManager.LoadScene("2ships_____atthesametime");
	}

	public void Quit()
	{
		Application.Quit();
	}
}
