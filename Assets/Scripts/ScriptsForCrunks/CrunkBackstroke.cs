using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrunkBackstroke : MonoBehaviour
{
	Crunk crunk = null;
	CrunkAnimation crunkAnimation;

	bool wasStroking = false;

	bool reachedPeak = false;

	public float minPush = 10;
	public float maxPush = 100;
	public float maxTime = 1;
	public float elapsedStroke = 0;

	private void Start()
	{
		crunk = GetComponent<Crunk>();
		crunkAnimation = GetComponent<CrunkAnimation>();
	}

	private void Update()
	{
		bool stroking = false;
		if (crunk.parentShip == null && crunk.grabbedModule == null)
		{
			stroking = Input.GetAxis($"Charge{crunk.playerNumber}") > Helper.Epsilon;

			if (stroking)
			{
				if (!wasStroking)
				{
					reachedPeak = false;
					elapsedStroke = 0;
					crunkAnimation.StartBackstroking();
				}

				if (reachedPeak)
				{
					elapsedStroke += Time.deltaTime;
				}
			}
			else
			{
				if (wasStroking)
				{
					crunkAnimation.StopBackstroking();

					if (elapsedStroke > 0 && maxTime > 0)
					{
						var completion = Mathf.Clamp(elapsedStroke / maxTime, 0, 1);
						var force = (minPush * (1 - completion)) + (maxPush * completion);

						crunk.Mover.ApplyExternalForce(crunk.transform.forward * force);
					}
				}
			}
		}

		wasStroking = stroking;
	}

	public void ReachedPeak()
	{
		reachedPeak = true;
	}
}
