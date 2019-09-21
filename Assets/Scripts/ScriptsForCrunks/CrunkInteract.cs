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
			if (interacting)
			{
				var module = crunk.GetModule();
				if (module != null)
				{

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
	}
}
