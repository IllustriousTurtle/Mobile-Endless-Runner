using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
public class BaseEnemyBehaviour : MonoBehaviour
{

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

		Debug.Log(collision.name);
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = new Color(1, 0, 0, 0.3f);
		Gizmos.DrawCube(transform.position, Vector3.one);
	}
}