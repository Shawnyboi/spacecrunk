using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrunkPump : MonoBehaviour
{
	Crunk crunk;
	bool alreadyPumped = false;

	private void Start()
	{
		crunk = GetComponent<Crunk>();
	}

	private void Update()
	{
		if (Input.GetAxis($"Charge{crunk.playerNumber}") > Helper.Epsilon)
		{
			if (!alreadyPumped && crunk.Mover.Stationary && crunk.nearbySlot?.Module?.CurrentCrunk == crunk)
			{
				bool pumping = crunk.nearbySlot.Module.PumpUp();
			}
		}
		else
		{
			alreadyPumped = false;
		}
	}
}
