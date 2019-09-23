using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrunkBackstroke : MonoBehaviour
{
	Crunk crunk = null;
	CrunkAnimation crunkAnimation;

	bool wasStroking = false;

	private void Start()
	{
		crunk = GetComponent<Crunk>();
		crunkAnimation = GetComponent<CrunkAnimation>();
	}

	private void Update()
	{
		bool stroking = false;
		if (crunk.parentShip == null)
		{
			stroking = Input.GetAxis($"Charge{crunk.playerNumber}") > Helper.Epsilon;

			if (stroking)
			{
				if (!wasStroking)
				{
					crunkAnimation.StartBackstroking();
				}

			}
			else
			{
				if (wasStroking)
				{
					crunkAnimation.StopBackstroking();
				}
			}
		}

		wasStroking = stroking;
	}
}
