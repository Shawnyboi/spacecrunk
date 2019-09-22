using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrunkInteract : MonoBehaviour
{
	bool interacting = false;// these flags should be enum states
	bool holding = false;
	bool canHold = true;
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
			interacting = !holding;
		}
		else
		{
			interactTime = 0;
			canHold = true;
			if (interacting)
			{
				if (crunk.nearbyAirlock != null)
				{
					if (crunk.heldModule != null)
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
				else if (crunk.heldModule != null)
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
			holding = false;
		}

		if (interacting)
		{
			if (interactTime >= holdThreshold)
			{
				interacting = false;
				holding = canHold;
			}

			interactTime += Time.deltaTime;
		}

		if (holding)
		{
			if (crunk.nearbySlot != null)
			{
				if (crunk.heldModule != null)
				{
					crunk.nearbySlot.AddModule(crunk.heldModule);
					crunk.DropModule();
					holding = false;
					canHold = false;
				}
				else if (crunk.nearbySlot.Module != null)
				{
					crunk.nearbySlot.RemoveModule();
					holding = false;
					canHold = false;
				}
			}
		}
	}
}
