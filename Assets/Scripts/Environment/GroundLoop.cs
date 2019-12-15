using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundLoop : MonoBehaviour
{
	[SerializeField]
	GameObject groundPiece, groundPieceTwo;

	[SerializeField]
	float spawnOffset = 38;

	[SerializeField]
	float respawnTriggerPosition = -22f;

	[SerializeField]
	float maxMovementSpeed;
	[SerializeField]
	float movementSpeedIncrease;

	[SerializeField]
	float movementSpeed;

	[SerializeField]
	GameObject[] hazards;
	[SerializeField]
	float hazardHeightOffset = 19.72024f;

	[SerializeField]
	GameObject[] enemyEagles;
	[SerializeField]
	float eagleSpawnHeight = 5.0f;

	[SerializeField]
	GameObject[] enemyFrogs;
	[SerializeField]
	float frogMaxSpawnDistance = 5.0f;

	[SerializeField]
	bool isSky;

	private void Update()
	{
		if (groundPiece.transform.position.x < respawnTriggerPosition && groundPiece.transform.position.x > respawnTriggerPosition - 1)
		{
			groundPieceTwo.transform.position = new Vector3(spawnOffset, groundPieceTwo.transform.position.y);
			GenerateHazards(groundPieceTwo);
		}

		if (groundPieceTwo.transform.position.x < respawnTriggerPosition && groundPieceTwo.transform.position.x > respawnTriggerPosition - 1)
		{
			groundPiece.transform.position = new Vector3(spawnOffset, groundPiece.transform.position.y);
			GenerateHazards(groundPiece);
		}

		groundPiece.transform.position -= (Vector3)Vector2.right * movementSpeed;
		groundPieceTwo.transform.position -= (Vector3)Vector2.right * movementSpeed;

		if (movementSpeed < maxMovementSpeed)
		{
			movementSpeed += movementSpeedIncrease * Time.fixedDeltaTime;
		}
	}

	void GenerateHazards(GameObject areaToGenerate)
	{
		if (isSky)
		{
			return;
		}

		//Clear chunk before adding more hazards
		RemoveGeneratedHazards(enemyEagles, new Vector2(0, -15));
		RemoveGeneratedHazards(enemyFrogs, new Vector2(0, -15));
		RemoveGeneratedHazards(hazards, new Vector2(0, -15));

		//Make sure we're moving fast enough to clear certain objects otherwise just spawn enemies. (Covers difficulty curve as well)
		if (movementSpeed > maxMovementSpeed * 0.5f)
		{
			GameObject hazard = hazards[Random.Range(0, hazards.Length - 1)];
			hazard.transform.position = Vector3.zero + areaToGenerate.transform.position + new Vector3(-3, 0, 0);
			hazard.GetComponent<HazardGenerator>().GenerateHazards();
			hazard.transform.parent = areaToGenerate.transform;
		}
		else
		{
			GameObject activeEnemy;
			if (Random.Range(0f, 1f) > 0.25f)
			{

				activeEnemy = enemyEagles[Random.Range(0, enemyEagles.Length - 1)];
				if (activeEnemy != null)
				{
					activeEnemy.transform.position = (Vector3.zero + areaToGenerate.transform.position) + new Vector3(0, Random.Range(-eagleSpawnHeight, eagleSpawnHeight));
				}
			}
			else
			{
				activeEnemy = enemyFrogs[Random.Range(0, enemyFrogs.Length - 1)];
				if (activeEnemy != null)
				{
					activeEnemy.transform.position = (Vector3.zero + areaToGenerate.transform.position) + new Vector3(Random.Range(-frogMaxSpawnDistance, frogMaxSpawnDistance), -6);
				}
			}

			activeEnemy.transform.parent = areaToGenerate.transform;

		}
	}

	void RemoveGeneratedHazards(GameObject[] hazards, Vector2 offset)
	{
		foreach (GameObject hazard in hazards)
		{
			if (hazard.transform.parent != null)
			{
				hazard.transform.parent = null;
			}

			if (hazard.transform.position != (Vector3)offset)
			{
				hazard.transform.position = offset;
			}
		}
	}
}
