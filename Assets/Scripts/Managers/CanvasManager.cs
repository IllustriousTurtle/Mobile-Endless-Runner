using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using TMPro;

public class CanvasManager : MonoBehaviour
{
	private static CanvasManager instance = null;
	private static readonly object padlock = new object();

	CanvasManager() { }

	public static CanvasManager Instance
	{
		get
		{
			lock (padlock)
			{
				if (instance == null)
				{
					instance = FindObjectOfType<CanvasManager>();

					if (instance == null)
					{
						instance = new GameObject().AddComponent<CanvasManager>();
					}
				}
				return instance;
			}
		}
	}

	[SerializeField]
	TextMeshProUGUI playerGemScore;
	[SerializeField]
	Canvas deathCanvas;
	[SerializeField]
	Canvas playCanvas;
	public void PlayerDied(int playerScore)
	{
		deathCanvas.gameObject.SetActive(true);
		playCanvas.gameObject.SetActive(false);
		playerGemScore.text = "You Collected " + playerScore.ToString() + " Gems!";
	}

	public void Button_Retry()
	{
		SceneManager.LoadScene("RunnerScene");
	}

	public void Button_QuitToMenu()
	{
		SceneManager.LoadScene("Menu");
	}

	public void Button_QuitGame()
	{
		Application.Quit();
	}
}
