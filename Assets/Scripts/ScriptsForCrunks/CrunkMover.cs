using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CrunkMover : MonoBehaviour
{
    Crunk crunk;
    Rigidbody body = null;
    Ship myShip;
    Vector3 lastFrameShipPosition;
    Vector3 currentFrameShipPosition;
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

    public void setGrounded(bool b) => grounded = b;

	public bool Stationary = false;

	Vector3 externalForce = Vector3.zero ;

	private void Start()
	{
        crunk = GetComponent<Crunk>();
        myShip = crunk.allyShip;
        if (myShip != null)
        {
            lastFrameShipPosition = myShip.transform.position;
        }
        body = GetComponent<Rigidbody>();

	}

	private void FixedUpdate()
	{
        if(myShip != null)
        {
            currentFrameShipPosition = myShip.transform.position;
        }
        
        var inShip = crunk.parentShip != null;
        var shipTranslation = new Vector3(0, 0, 0);
        if (inShip)
        {
            shipTranslation = currentFrameShipPosition - lastFrameShipPosition;
        }
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

		if (grounded)
		{
			body.position += shipTranslation;
		}

		externalForce = Vector3.zero;
        if (myShip != null)
        {
            lastFrameShipPosition = myShip.transform.position;
        }
      
    }

	public void ApplyExternalForce(Vector3 force)
	{
		externalForce += force;
	}
}
