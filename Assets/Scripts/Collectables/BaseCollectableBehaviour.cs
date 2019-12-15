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

	private void OnValidate()
	{
		//Sets Intractable Items up for Player overlapping.
		if (!GetComponent<BoxCollider2D>().isTrigger)
			GetComponent<BoxCollider2D>().isTrigger = true;
		if (!GetComponent<Rigidbody2D>().isKinematic)
			GetComponent<Rigidbody2D>().isKinematic = true;
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.GetComponent<PlayerController>() != null)
		{
			
		}
	}

}
