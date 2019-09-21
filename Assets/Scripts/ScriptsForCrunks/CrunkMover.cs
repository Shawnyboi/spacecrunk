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
	[SerializeField]
	float groundedDragFactor = 0.6f;
	[SerializeField]
	float floatingDragFactor = 0.8f;

	bool grounded = true;

	public bool Stationary = false;

	Vector3 externalForce = Vector3.zero ;

	private void Start()
	{
		body = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		//TODO Support rotation
		//TODO instead of directly moving character move a point out in front that the player seeks (or pivots to)

		var horizontal = Input.GetAxis("Horizontal");
		var vertical = Input.GetAxis("Vertical");

		body.AddForce(externalForce);

		var internalForce = (((horizontalAxis * horizontal) + (verticalAxis * vertical))).normalized * acceleration;

		if (body.velocity.sqrMagnitude < (maxSpeed * maxSpeed) && !Stationary)
		{
			body.AddForce(internalForce);
		}

		if ((internalForce.sqrMagnitude + externalForce.sqrMagnitude) < Helper.Epsilon)
		{
			body.velocity *= grounded ? groundedDragFactor : floatingDragFactor;
		}

		externalForce = Vector3.zero;
	}

	public void ApplyExternalForce(Vector3 force)
	{
		externalForce += force;
	}
}
