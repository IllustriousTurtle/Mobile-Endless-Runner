using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerController : MonoBehaviour
{

	[SerializeField]
	float gravityY = Physics.gravity.y;

	Rigidbody2D playerRB;
	Animator playerAnimator;

	bool isGrounded;

	[SerializeField]
	Vector2 groundedOverlapScale;
	[SerializeField]
	Vector2 groundOverlapOffset;

	bool isMoving;

	//[SerializeField]
	//float maxUpwardsVelocity = 10.0f;

	[SerializeField]
	float jumpForce;

	[SerializeField]
	float maxJumpHeight;
	[SerializeField]
	float jumpMultiplier;
	[SerializeField]
	float fallMultiplier;

	[HideInInspector]
	public int playerScore;

	private void OnValidate()
	{
		//Set player to be drawn after all environment objects. Stops player disappearing.
		GetComponent<SpriteRenderer>().sortingOrder = 1001;
	}

	void Start()
	{
		playerRB = GetComponent<Rigidbody2D>();
		playerAnimator = GetComponent<Animator>();

		//Lock rotation to stop player falling
		playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
		gravityY = Physics2D.gravity.y;
	}

	private void Update()
	{
		isGrounded = CheckGrounded();
		isMoving = !isGrounded;

		//Debugging otherwise use touch inputs
		if (Application.isEditor)
		{
			if (isGrounded)
			{
				if (Input.GetMouseButtonDown(0))
				{
					playerRB.velocity += Vector2.up * jumpForce;
				}
			}
		}

		if (playerRB.velocity.y < 0)
		{
			playerRB.velocity += Vector2.up * gravityY * fallMultiplier * Time.deltaTime;
		}
		else if (playerRB.velocity.y > 0 && !Input.GetMouseButton(0))
		{
			playerRB.velocity += Vector2.up * gravityY * jumpMultiplier * Time.deltaTime;
		}
	}

	private void FixedUpdate()
	{
		isGrounded = CheckGrounded();
		isMoving = !isGrounded;
	}

	bool CheckGrounded()
	{
		if (Physics2D.Raycast(transform.position, -transform.up, 1.5f))
		{
			return true;
		}
		return false;
	}
}
