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
	public Module grabbedModule;
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
        Debug.Log("Entered collider of " + other.name);
		var moduleCollider = other.GetComponent<ModuleInteractCollider>();
		var airlock = other.GetComponent<Airlock>();

		if (moduleCollider != null)
		{
            Debug.Log("Entered collider was a module");
			if (nearbyModule == null ||
				(moduleCollider.targetModule.transform.position - transform.position).sqrMagnitude < (nearbyModule.transform.position - transform.position).sqrMagnitude)
			{
				nearbyModule = moduleCollider.targetModule;
			}
		}
		else if (airlock != null)
		{
			Debug.Log("Entered collider was the module");
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
			Debug.Log("exitting module collider");
			if (nearbyModule == null ||
				(moduleCollider.targetModule.transform.position - transform.position).sqrMagnitude < (nearbyModule.transform.position - transform.position).sqrMagnitude)
			{
				nearbyModule = moduleCollider.targetModule;
			}
		}
		else if (airlock != null && airlock == nearbyAirlock)
		{
			nearbyAirlock = null;
		}
		else
		{
			var slot = FindModuleSlot(other);
			Debug.Log("attempting to exit slot " + slot);
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
        Debug.Log("Picking Up module : " + module.name);
		grabbedModule = module;
        grabbedModule.GetComponent<Rigidbody>().isKinematic = true;
		grabbedModule.transform.parent = moduleContainer;
	}

	public void DropModule()
	{
        Debug.Log("Dropping module : " + grabbedModule.name);
		grabbedModule.transform.parent = null;
        grabbedModule.GetComponent<Rigidbody>().isKinematic = false;
		grabbedModule = null;
	}
}
