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
	TextMeshProUGUI playerHighScore;
	[SerializeField]
	Canvas deathCanvas;
	[SerializeField]
	Canvas playCanvas;

	//Toggles Game Over and Score canvas
	public void PlayerDied(int playerScore)
	{
		ScoreManager.Instance.LoadScore();
		if (deathCanvas != null)
		{
			deathCanvas.gameObject.SetActive(true);
		}

		if (playCanvas != null)
		{
			playCanvas.gameObject.SetActive(false);
		}

		playerGemScore.text = "You Collected " + playerScore.ToString() + " Gems!";

		if (ScoreManager.Instance.highScore < ScoreManager.Instance.playerScore)
		{
			if (ScoreManager.Instance.highScore <= 0)
			{
				playerHighScore.text = "Previous Best: " + 0;
			}
			else
			{
				playerHighScore.text = "Previous Best: " + playerScore;
			}

			ScoreManager.Instance.SaveScore();
		}
		else
		{
			playerHighScore.text = "Current Best: " + playerScore;
		}
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
