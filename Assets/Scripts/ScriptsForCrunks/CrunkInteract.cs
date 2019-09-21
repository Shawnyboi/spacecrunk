using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrunkInteract : MonoBehaviour
{
	bool interacting = false;
	bool holding = false;
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
			if (interacting)
			{



				if (crunk.heldModule != null)
				{
					crunk.heldModule = null;
				}
				else if (crunk.nearbySlot != null)
				{
					var slottedModule = crunk.nearbySlot.Module;
					Debug.Log(slottedModule == null);
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
					crunk.heldModule = crunk.nearbyModule;
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
				holding = true;
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
				}
				else if (crunk.nearbySlot.Module != null)
				{
					crunk.nearbySlot.RemoveModule();
				}
			}
		}
	}
}
