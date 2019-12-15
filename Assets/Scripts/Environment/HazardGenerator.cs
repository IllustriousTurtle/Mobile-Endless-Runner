using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class HazardGenerator : MonoBehaviour
{
	[SerializeField]
	GameObject[] generalEnemies;

	[SerializeField]
	GameObject[] generalHazards;

	[SerializeField]
	GameObject[] hazardToggledEnemies;

	[SerializeField]
	GameObject[] enemyToggledHazards;

	[ContextMenu("Test Generate")]
	public void GenerateHazards()
	{
		//Disable all objects
		ToggleAllObjects(generalEnemies);
		ToggleAllObjects(generalHazards);
		ToggleAllObjects(hazardToggledEnemies);
		ToggleAllObjects(enemyToggledHazards);

		//Force random state using current time
		Random.InitState(System.DateTime.Now.Millisecond);

		if (generalEnemies.Length > 0)
		{
			generalEnemies[Random.Range(0, generalEnemies.Length)].SetActive(true);
		}

		if (generalHazards.Length > 0)
		{
			generalHazards[Random.Range(0, generalHazards.Length)].SetActive(true);
		}


		//Toggle between Hazard and Enemy generation
		if (Random.Range(0, 10) < 2.5)
		{
			if (hazardToggledEnemies.Length > 0)
			{
				hazardToggledEnemies[Random.Range(0, hazardToggledEnemies.Length)].SetActive(true);
			}
		}
		else
		{
			ToggleAllObjects(enemyToggledHazards, true);
		}
	}

	void ToggleAllObjects(GameObject[] toggledObjects, bool setActive = false)
	{
		foreach (GameObject go in toggledObjects)
		{
			if (go != null)
			{
				go.SetActive(setActive);
			}
		}
	}
}
