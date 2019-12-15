using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerController : MonoBehaviour
{

	[SerializeField]
	float yGravity = Physics.gravity.y;

	Rigidbody2D playerRB;
	Animator playerAnimator;

	bool isGrounded;
	bool isMoving;

	[SerializeField]
	float jumpForce;

	[SerializeField]
	float jumpMultiplier;
	[SerializeField]
	float fallMultiplier;

	float playerXPosition;

	private void OnValidate()
	{
		//Set player to be drawn after all environment objects. Stops player disappearing.
		GetComponent<SpriteRenderer>().sortingOrder = 1001;
	}

	void Start()
	{
		playerRB = GetComponent<Rigidbody2D>();
		playerAnimator = GetComponent<Animator>();

		//Lock rotation to stop player rolling
		playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
		yGravity = Physics2D.gravity.y;
		playerXPosition = transform.position.x;
	}

	private void Update()
	{
		Jump();
		UpdateAnimationStates();

		transform.position = new Vector3(playerXPosition, transform.position.y);
	}

	void Jump()
	{
		if (isGrounded)
		{
			if (Input.GetMouseButtonDown(0) || (Application.isMobilePlatform && Input.GetTouch(0).phase == TouchPhase.Began))
			{
				playerRB.velocity += Vector2.up * jumpForce;
			}
		}

		//Apply downwards force for arc
		if (playerRB.velocity.y < 0)
		{
			playerRB.velocity += Vector2.up * yGravity * fallMultiplier * Time.deltaTime;
		}
		else if (playerRB.velocity.y > 0 && (!Input.GetMouseButton(0) || (Application.isMobilePlatform && Input.GetTouch(0).phase == TouchPhase.Ended)))
		{
			playerRB.velocity += Vector2.up * yGravity * jumpMultiplier * Time.deltaTime;
		}

	}

	void UpdateAnimationStates()
	{
		isGrounded = CheckGrounded();
		isMoving = isGrounded;

		playerAnimator.SetBool("isMoving", isMoving);
		playerAnimator.SetBool("isGrounded", isGrounded);
	}

	/// <summary>
	/// Checks if player is grounded for jumping
	/// </summary>
	bool CheckGrounded()
	{
		if (Physics2D.Raycast(transform.position, -transform.up, 1.5f))
		{
			return true;
		}
		return false;
	}

	private void OnDestroy()
	{
		CanvasManager.Instance.PlayerDied(ScoreManager.Instance.playerScore);
	}
}
