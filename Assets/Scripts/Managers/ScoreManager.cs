using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;

using TMPro;

[System.Serializable]
public class SaveLoadData
{
	public int score;
}

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

	public int highScore;

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

	private void Start()
	{
		LoadScore();
	}

	string saveLoadDataID = "runnerSave.json";
	SaveLoadData saveData = new SaveLoadData();

	//Writes data to Json file on Mobile/PC Devices
	public void SaveScore()
	{
		saveData = new SaveLoadData();
		saveData.score = playerScore;
		string jsonData = JsonUtility.ToJson(saveData);
		if (!File.Exists(Application.persistentDataPath + "/" + saveLoadDataID))
		{
			File.Create(Application.persistentDataPath + "/" + saveLoadDataID).Dispose();
		}

		byte[] fileData = System.Text.Encoding.ASCII.GetBytes(jsonData);
		File.WriteAllBytes(Application.persistentDataPath + "/" + saveLoadDataID, fileData);
	}

	//Reads data from Json file on Mobile/PC Devices
	public void LoadScore()
	{
		saveData = new SaveLoadData();
		if (!File.Exists(Application.persistentDataPath + "/" + saveLoadDataID))
		{
			return;
		}

		string fileData = File.ReadAllText(Application.persistentDataPath + "/" + saveLoadDataID);

		JsonUtility.FromJsonOverwrite(fileData, saveData);
		highScore = saveData.score;
	}
}
