using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody),typeof(CrunkAnimation))]
public class CrunkMover : MonoBehaviour
{
    public Crunk crunk;
    CrunkAnimation crunkAnimation;
    Rigidbody body = null;
    Ship myShip;
    Vector3 lastFrameShipPosition;
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
	bool wasGrounded = true;
    bool moving = false;

    public void setGrounded(bool b) => grounded = b;

	Vector3 externalForce = Vector3.zero ;

	private void Start()
	{
        crunk = GetComponent<Crunk>();
        crunkAnimation = GetComponent<CrunkAnimation>();
        myShip = crunk.allyShip;
        if (myShip != null)
        {
            lastFrameShipPosition = crunk.parentShip.transform.InverseTransformPoint(transform.position);
        }
        body = GetComponent<Rigidbody>();

	}

	private void FixedUpdate()
	{
		if (grounded && wasGrounded && crunk.parentShip != null)
		{
			body.position = crunk.parentShip.transform.TransformPoint(lastFrameShipPosition) + body.velocity * Time.deltaTime;
		}

		var horizontal = Input.GetAxis($"Horizontal{crunk.playerNumber}");
		var vertical = Input.GetAxis($"Vertical{crunk.playerNumber}");

        if((horizontal != 0 || vertical != 0) && crunk.lockedSlot == null)
        {
            if (moving == false)
            {
                crunkAnimation.StartMoving();
                moving = true;
            }
        }
        else
        {
            if(moving == true)
            {
                crunkAnimation.StopMoving();
                moving = false;
            }
        }

		body.AddForce(externalForce, ForceMode.Impulse);

		var internalForce = (((horizontalAxis * horizontal) + (verticalAxis * vertical))).normalized * acceleration;

		Vector3 lookAt = transform.position + transform.forward;
		if (internalForce.sqrMagnitude > 0)
		{
			lookAt = transform.position + internalForce;
		}

		if (crunk.lockedSlot == null)
		{
			transform.LookAt(lookAt);
		}

		if (body.velocity.sqrMagnitude < (maxSpeed * maxSpeed) && crunk.lockedSlot == null)
		{
			body.AddForce(internalForce);
		}

		if ((internalForce.sqrMagnitude + externalForce.sqrMagnitude) < Helper.Epsilon)
		{

			body.velocity *= grounded ? groundedDragFactor : floatingDragFactor;
		}

		externalForce = Vector3.zero;
        if (myShip != null)
        {
            lastFrameShipPosition = myShip.transform.InverseTransformPoint(body.position);
        }
		wasGrounded = grounded;
    }

	public void ApplyExternalForce(Vector3 force)
	{
		externalForce += force;
	}
}
