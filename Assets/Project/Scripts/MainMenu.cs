using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public void NewGame()
	{
		SceneManager.LoadScene("Test3");
	}
	public void SettingsGame()
	{
		Debug.Log("SettingsGame");
	}
	public void ExitGame()
	{
		Application.Quit();
	}
}
