using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
public class BaseEnemyBehaviour : MonoBehaviour
{
	//Kills player on overlap and slows environment to a stop
	//or removes overlapping enemies
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetComponent<PlayerController>() != null)
		{
			FindObjectOfType<GroundLoop>().canMove = false;
			FindObjectOfType<SkyLoop>().canMove = false;

			AudioManager.Instance.PlayerDeath();
			Destroy(collision.gameObject);
		}
		else if (collision.GetComponent<BaseEnemyBehaviour>() != null)
		{
			if (collision.gameObject != gameObject)
			{
				collision.transform.position = new Vector3(0, -15, 0);
			}
		}
	}
}