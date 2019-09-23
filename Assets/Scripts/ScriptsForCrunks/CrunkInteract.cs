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
		if (Input.GetAxis($"Interact{crunk.playerNumber}") > Helper.Epsilon)
		{
			if (interactTime == 0)
			{
				if (crunk.nearbySlot != null)
				{
					var slotCollider = crunk.allyShip.GetColliderFromModuleSlot(crunk.nearbySlot);
					if (slotCollider != null)
					{
						if (crunk.nearbySlot.Module == null)
						{
							slotCollider.GetComponent<ModuleSlotAnimation>().BeginAttaching();
						}
						else
						{
							slotCollider.GetComponent<ModuleSlotAnimation>().BeginDettaching();
						}
					}
				}
			}

			interacting = !holdingDownButton;
		}
		else
		{
			interactTime = 0;
			canHoldButton = true;

			var slotCollider = crunk.allyShip.GetColliderFromModuleSlot(crunk.nearbySlot);
			if (slotCollider != null)
			{
				slotCollider.GetComponent<ModuleSlotAnimation>().StopTransitioning();
			}

			ModuleSlot nearbySlot = crunk.lockedSlot ?? crunk.nearbySlot;

			if (interacting)
			{
				if (crunk.nearbyAirlock != null)
				{
					if (crunk.lockedSlot == null)
					{
						if (crunk.nearbyAirlock.GetTeam() == crunk.GetTeam())
						{
							if (crunk.grabbedModule != null)
							{
								crunk.DropModule(true);
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
					}
				}
				else if (crunk.grabbedModule != null)
				{
					crunk.DropModule(true);
				}
				else if (nearbySlot != null)
				{
                    if (nearbySlot.GetShip().GetTeam() == crunk.GetTeam())
                    {
                        var slottedModule = nearbySlot.Module;

						if (slottedModule != null)
                        {
                            if (slottedModule.IsLockedIn())
                            {
                                slottedModule.LockOut();

								// Turn off hinter on module slot.
								var hinter = slottedModule.GetComponentInParent<EventsForToggle>();
								if (hinter != null)
								{
									hinter.TriggerPositive();
								}
							}
                            else
                            {
								slottedModule.LockIn(crunk, nearbySlot);
								crunk.transform.LookAt(slottedModule.transform.position);

								// Turn off hinter on module slot.
								var hinter = slottedModule.GetComponentInParent<EventsForToggle>();
								if (hinter != null)
								{
									hinter.TriggerNegative();
								}
                            }
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

		if (holdingDownButton && canHoldButton && crunk.lockedSlot == null)
		{
			if (crunk.nearbySlot != null && crunk.parentShip == null)
			{
				if (crunk.grabbedModule != null)
				{
					AttachModuleToShipFromHand();
				}
				else if (crunk.nearbySlot.Module != null && crunk.parentShip == null)
				{
					var slotAnimation = crunk.nearbySlot.Module.GetComponentInParent<ModuleSlotAnimation>();
					if (slotAnimation)
					{
						slotAnimation.FinishDettaching();
					}

					Module detachingModule = crunk.nearbySlot.Module;
					crunk.nearbySlot.RemoveModule();
					crunk.PickupModule(detachingModule);
					holdingDownButton = false;
					canHoldButton = false;

				}
			}
		}
	}

    private void AttachModuleToShipFromHand()
    {
        Module m = crunk.grabbedModule;
        crunk.DropModule(false);
        crunk.nearbySlot.AddModule(m);
		holdingDownButton = false;
		canHoldButton = false;

		var slotAnimation = m.GetComponentInParent<ModuleSlotAnimation>();
		if (slotAnimation)
		{
			slotAnimation.FinishAttaching();
		}
	}
}
