using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class ScoreManager : MonoBehaviour
{
	private static ScoreManager instance = null;
	private static readonly object padlock = new object();

	ScoreManager() { }

	public static ScoreManager Instance
	{
		get
		{
			lock (padlock)
			{
				if (instance == null)
				{
					instance = FindObjectOfType<ScoreManager>();

					if (instance == null)
					{
						instance = new GameObject().AddComponent<ScoreManager>();
					}
				}
				return instance;
			}
		}
	}

	[SerializeField]
	TextMeshProUGUI playerScoreText;

	private int PlayerScore;

	[HideInInspector]
	public int playerScore
	{
		get
		{
			return PlayerScore;
		}
		set
		{
			PlayerScore = value;
			playerScoreText.text = "Player Score: " + PlayerScore.ToString();
		}
	}
}
