using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Loops ground chunks and spawns Enemies, Collectables and Hazards
/// </summary>
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

	[HideInInspector]
	public float movementSpeed;

	[SerializeField]
	GameObject[] hazards;
	[SerializeField]
	float hazardHeightOffset = 19.72024f;

	[SerializeField]
	GameObject[] enemyEagles;
	[SerializeField]
	Vector2 eagleSpawnPosition = new Vector2(5.0f, 3.0f);

	[SerializeField]
	GameObject[] enemyFrogs;
	[SerializeField]
	float frogMaxSpawnDistance = 5.0f;

	[HideInInspector]
	public bool canMove = true;

	private void Update()
	{
		if (!canMove)
		{
			//Slows camera down on player death
			if (movementSpeed > 0.1f)
			{
				movementSpeed = Mathf.Lerp(movementSpeed, 0, 2.5f * Time.deltaTime);
			}
			else
			{
				movementSpeed = 0.0f;
			}
		}

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

		//Move environment left constantly by movementSpeed
		groundPiece.transform.position -= (Vector3)Vector2.right * movementSpeed;
		groundPieceTwo.transform.position -= (Vector3)Vector2.right * movementSpeed;

		if (movementSpeed < maxMovementSpeed)
		{
			movementSpeed += movementSpeedIncrease * Time.fixedDeltaTime;
		}
	}

	void GenerateHazards(GameObject areaToGenerate)
	{
		//Clear chunk before adding more hazards
		RemoveGeneratedHazards(enemyEagles, new Vector2(0, -15));
		RemoveGeneratedHazards(enemyFrogs, new Vector2(0, -15));
		RemoveGeneratedHazards(hazards, new Vector2(0, -15));

		//Make sure we're moving fast enough to clear certain objects otherwise just spawn enemies. (Covers difficulty curve as well)
		if (movementSpeed > maxMovementSpeed * 0.5f)
		{
			GameObject hazard = hazards[Random.Range(0, hazards.Length)];
			hazard.transform.position = Vector3.zero + areaToGenerate.transform.position + new Vector3(-3, 0, 0);
			hazard.GetComponent<HazardGenerator>().GenerateHazards();
			hazard.transform.parent = areaToGenerate.transform;
		}
		else
		{
			GameObject activeEnemy;
			for (int i = 0; i < 5; i++)
			{
				if (Random.Range(0f, 1f) > 0.25f)
				{

					activeEnemy = enemyEagles[Random.Range(0, enemyEagles.Length)];
					if (activeEnemy != null)
					{
						activeEnemy.transform.position = Vector3.zero;
						activeEnemy.transform.position = areaToGenerate.transform.position + new Vector3(Random.Range(-eagleSpawnPosition.x, eagleSpawnPosition.x), Random.Range(-eagleSpawnPosition.y, eagleSpawnPosition.y));
					}
				}
				else
				{
					activeEnemy = enemyFrogs[Random.Range(0, enemyFrogs.Length)];
					if (activeEnemy != null)
					{
						activeEnemy.transform.position = Vector3.zero;
						activeEnemy.transform.position = areaToGenerate.transform.position + new Vector3(Random.Range(-frogMaxSpawnDistance, frogMaxSpawnDistance), -6);
					}
				}

				activeEnemy.transform.parent = areaToGenerate.transform;
			}
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
