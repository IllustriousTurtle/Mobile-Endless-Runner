using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class BaseCollectableBehaviour : MonoBehaviour
{

	[SerializeField] int scoreWorth;
	[SerializeField] GameObject collectEffect;

	private void OnValidate()
	{
		//Sets Intractable Items up for Player overlapping.
		if (!GetComponent<BoxCollider2D>().isTrigger)
			GetComponent<BoxCollider2D>().isTrigger = true;
		if (!GetComponent<Rigidbody2D>().isKinematic)
			GetComponent<Rigidbody2D>().isKinematic = true;
	}

	//Increase players score, instantiate pickup effect and make pickup invisible
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetComponent<PlayerController>() != null)
		{
			ScoreManager.Instance.playerScore += scoreWorth;
			GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);

			Instantiate(collectEffect, transform);
			AudioManager.Instance.CollectablePickup();

			StartCoroutine(SpawnParticle());
		}
	}

	//Destroy pickup
	IEnumerator SpawnParticle()
	{
		yield return new WaitForSeconds(0.10f);
		Destroy(gameObject);
	}
}
