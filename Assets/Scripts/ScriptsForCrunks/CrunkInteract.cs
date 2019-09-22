using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrunkInteract : MonoBehaviour
{
	bool interacting = false;// these flags should be enum states
	bool holdingDownButton = false;
	bool canHoldButton = true;
	float interactTime = 0;

	[SerializeField]
	float holdThreshold = 1;

	Crunk crunk = null;

	private void Start()
	{
		crunk = GetComponent<Crunk>();
	}

	private void Update()
	{
		if (Input.GetAxis("Interact") > Helper.Epsilon)
		{
			interacting = !holdingDownButton;
		}
		else
		{
			interactTime = 0;
			canHoldButton = true;
			if (interacting)
			{
				if (crunk.nearbyAirlock != null)
				{
					if (crunk.grabbedModule != null)
					{
						crunk.DropModule();
					} 

					if (crunk.parentShip == null)
					{
						crunk.nearbyAirlock.EnterShip(crunk.Mover);
					}
					else
					{
						crunk.nearbyAirlock.LeaveShip(crunk.Mover);
					}
				}
				else if (crunk.grabbedModule != null)
				{
					crunk.DropModule();
				}
				else if (crunk.nearbySlot != null)
				{
					var slottedModule = crunk.nearbySlot.Module;
					if (slottedModule != null)
					{
						if (slottedModule.IsLockedIn())
						{
							slottedModule.LockOut();
						}
						else
						{
							slottedModule.LockIn(crunk);
						}
					}
				}
				else if (crunk.nearbyModule != null)
				{
					crunk.PickupModule(crunk.nearbyModule);
				}
			}

			interacting = false;
			holdingDownButton = false;
		}

		if (interacting)
		{
			if (interactTime >= holdThreshold)
			{
				interacting = false;
				holdingDownButton = canHoldButton;
			}

			interactTime += Time.deltaTime;
		}

		if (holdingDownButton)
		{
			if (crunk.nearbySlot != null)
			{
				if (crunk.grabbedModule != null)
				{
                    Module m = crunk.grabbedModule;
                    crunk.DropModule();
                    crunk.nearbySlot.AddModule(m);
					holdingDownButton = false;
					canHoldButton = false;
				}
				else if (crunk.nearbySlot.Module != null)
				{
					crunk.nearbySlot.RemoveModule();
					holdingDownButton = false;
					canHoldButton = false;
				}
			}
		}
	}
}
