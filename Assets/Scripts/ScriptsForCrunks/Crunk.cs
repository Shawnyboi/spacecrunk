using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CrunkMover), typeof(CrunkInteract))]
public class Crunk : MonoBehaviour
{
	//todo this should be private
	public Ship allyShip = null;
	public Ship enemyShip = null;
	public Ship parentShip = null;
	public ModuleSlot nearbySlot;
	public Module nearbyModule;
	public Module heldModule;

	private CrunkMover mover = null;
	public CrunkMover Mover
	{
		get
		{
			if (mover == null)
			{
				mover = GetComponent<CrunkMover>();
			}
			return mover;
		}
	}
	private CrunkInteract interact = null;
	public CrunkInteract Interact
	{
		get
		{
			if (interact == null)
			{
				interact = GetComponent<CrunkInteract>();
			}
			return interact;
		}
	}


	/*public Module GetModule()
	{
		if (heldModule != null)
		{
			return heldModule;
		}
		else if (nearbyModule != null)
		{
			return nearbyModule;
		}
		else if (nearbySlot?.Module != null)
		{
			return nearbySlot.Module;
		}

		return null;
	}*/

	private void OnTriggerEnter(Collider other)
	{
		var slot = FindModuleSlot(other);
		if (slot != null)
		{
			nearbySlot = slot;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		var slot = FindModuleSlot(other);
		if (slot == nearbySlot)
		{
			nearbySlot = null;
		}
	}

	private ModuleSlot FindModuleSlot(Collider other)
	{
		var slot = allyShip?.GetModuleSlotAtCollider(other);
		if (slot == null)
		{
			slot = enemyShip?.GetModuleSlotAtCollider(other);
		}

		return slot;
	}

	public void PickupModule(Module module)
	{
		heldModule = module;
	}

	public void DropModule()
	{
		heldModule = null;
	}
}
