using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyLoop : MonoBehaviour
{
	[SerializeField]
	GameObject skyOne, skyTwo;

	[SerializeField]
	float spawnOffset = 33;
	[SerializeField]
	float respawnTriggerPosition = -4f;

	[SerializeField]
	float maxMovementSpeed = 0.05f;
	[SerializeField]
	float movementSpeedIncrease = 0.001f;

	[HideInInspector]
	public float movementSpeed;

	[HideInInspector]
	public bool canMove;

    void Update()
    {
		if (!canMove)
		{
			movementSpeed = Mathf.Lerp(movementSpeed, 0, 5 * Time.deltaTime);
		}

		//Swap sky positions based on distance from camera edge
		if (skyOne.transform.position.x < respawnTriggerPosition && skyOne.transform.position.x > respawnTriggerPosition - 1)
		{
			skyTwo.transform.position = new Vector3(spawnOffset, skyTwo.transform.position.y);
		}

		if (skyTwo.transform.position.x < respawnTriggerPosition && skyTwo.transform.position.x > respawnTriggerPosition - 1)
		{
			skyOne.transform.position = new Vector3(spawnOffset, skyOne.transform.position.y);
		}

		//Move environment left constantly by movementSpeed
		skyOne.transform.position -= (Vector3)Vector2.right * movementSpeed;
		skyTwo.transform.position -= (Vector3)Vector2.right * movementSpeed;

		if (movementSpeed < maxMovementSpeed)
		{
			movementSpeed += movementSpeedIncrease * Time.fixedDeltaTime;
		}
	}
}
