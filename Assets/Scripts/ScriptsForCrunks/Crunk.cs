using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crunk : MonoBehaviour
{
	//todo this should be private
	public Ship allyShip = null;
	public Ship enemyShip = null;
	public Ship parentShip = null;
	public ModuleSlot nearbySlot;
	public Module nearbyModule;
	public Module heldModule;

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
		var slot = allyShip.GetModuleSlotAtCollider(other);
		if (slot == null)
		{
			slot = enemyShip.GetModuleSlotAtCollider(other);
		}

		return slot;
	}
}
