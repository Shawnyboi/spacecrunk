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
    CrunkAnimation crunkAnimation;
	private void Start()
	{
		crunk = GetComponent<Crunk>();
        crunkAnimation = GetComponent<CrunkAnimation>();
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
			if (interacting && !crunk.Mover.Stationary)
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
			else
			{

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

		if (holdingDownButton && canHoldButton && !crunk.Mover.Stationary)
		{
			if (crunk.nearbySlot != null && crunk.parentShip == null)
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
