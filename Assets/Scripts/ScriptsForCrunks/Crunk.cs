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
	public Airlock nearbyAirlock;

	public Transform moduleContainer;

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

	private void Start()
	{
		if (moduleContainer == null)
		{
			moduleContainer = transform;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		var moduleCollider = other.GetComponent<ModuleInteractCollider>();
		var airlock = other.GetComponent<Airlock>();

		if (moduleCollider != null)
		{
			if (nearbyModule == null || 
				(moduleCollider.targetModule.transform.position - transform.position).sqrMagnitude < (nearbyModule.transform.position - transform.position).sqrMagnitude)
			{
				nearbyModule = moduleCollider.targetModule;
			}
		}
		else if (airlock != null)
		{
			Debug.Log("found airlock");
			nearbyAirlock = airlock;
		}
		else
		{
			var slot = FindModuleSlot(other);
			if (slot != null)
			{
				nearbySlot = slot;
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		var moduleCollider = other.GetComponent<ModuleInteractCollider>();
		var airlock = other.GetComponent<Airlock>();

		if (moduleCollider != null)
		{
			if (nearbyModule == null ||
				(moduleCollider.targetModule.transform.position - transform.position).sqrMagnitude < (nearbyModule.transform.position - transform.position).sqrMagnitude)
			{
				nearbyModule = moduleCollider.targetModule;
			}
		}
		else if (airlock != nearbyAirlock)
		{
			nearbyAirlock = null;
		}
		else
		{
			var slot = FindModuleSlot(other);
			if (slot == nearbySlot)
			{
				nearbySlot = null;
			}
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
		heldModule.transform.parent = moduleContainer;
	}

	public void DropModule()
	{
		heldModule.transform.parent = null; // TODO this should go somewhere or disconnect the joint
		heldModule = null;
	}
}
