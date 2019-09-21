using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CrunkMover : MonoBehaviour
{
	Rigidbody body = null;
	[SerializeField]
	Vector3 horizontalAxis = Vector3.right;
	[SerializeField]
	Vector3 verticalAxis = Vector3.up;
	[SerializeField]
	float maxSpeed = 10;
	[SerializeField]
	float acceleration = 10;

	bool grounded = true;

	private void Start()
	{
		body = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		var horizontal = Input.GetAxis("Horizontal");
		var vertical = Input.GetAxis("Vertical");

		var force = (((horizontalAxis * horizontal) + (verticalAxis * vertical))).normalized * acceleration;

		body.AddForce(force);

		if (body.velocity.sqrMagnitude > (maxSpeed * maxSpeed))
		{
			body.velocity = body.velocity.normalized * maxSpeed;
		}
	}
}
