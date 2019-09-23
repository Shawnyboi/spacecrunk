using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CrunkMover), typeof(CrunkInteract), typeof(CrunkAnimation))]
public class Crunk : MonoBehaviour
{
	//todo this should be private
	public Ship allyShip = null;
	public Ship enemyShip = null;
	public Ship parentShip = null;
	public ModuleSlot nearbySlot;
	public ModuleSlot lockedSlot;
	public Module nearbyModule;
	public Module grabbedModule;
	public Airlock nearbyAirlock;

	public Transform moduleContainer;

	private CrunkMover mover = null;
    private CrunkAnimation crunkAnimation = null;
    private int team;
    private bool alive = true;

	public int playerNumber = 1;

    public int GetTeam() { return team; }

    private IEnumerator CheckOxygenPeriodically()
    {
        while (alive) {
            yield return new WaitForSeconds(2f);
            if (OutOfOxygen()){
                alive = false;
            }
        }
    }

    public bool isAlive()
    {
        return alive;
    }

    private bool OutOfOxygen()
    {
        if (allyShip != null)
        {
            return allyShip.NoMoreOxygen();
        }
        else
        {
            return true;
        }
    }
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
        team = allyShip.GetTeam();
        crunkAnimation = GetComponent<CrunkAnimation>();
		if (moduleContainer == null)
		{
			moduleContainer = transform;
		}
        StartCoroutine(CheckOxygenPeriodically());
	}

	private void OnTriggerEnter(Collider other)
	{
		var moduleCollider = other.GetComponent<ModuleInteractCollider>();
		var airlock = other.GetComponent<Airlock>();
        bool validInteraction = false;
		if (moduleCollider != null)
		{
			if (nearbyModule == null ||
				(moduleCollider.targetModule.transform.position - transform.position).sqrMagnitude < (nearbyModule.transform.position - transform.position).sqrMagnitude)
			{
				validInteraction = true;
				nearbyModule = moduleCollider.targetModule;
			}
		}
		else if (airlock != null)
		{
            if(airlock.GetTeam() == GetTeam())
            {
                validInteraction = true;
                nearbyAirlock = airlock;
            }            
		}
		else
		{
			var slot = FindModuleSlot(other);
			if (slot != null)
			{
				if (nearbySlot != null && nearbySlot != slot)
				{
					allyShip.GetColliderFromModuleSlot(nearbySlot).GetComponent<EventsForToggle>().TriggerNegative();
				}

				validInteraction = true;
				nearbySlot = slot;
			}
		}

        if (validInteraction)
        {
            var events = other.GetComponent<EventsForToggle>();
            if (events != null)
            {
                events.TriggerPositive();
            }
        }
	}

	private void OnTriggerExit(Collider other)
	{
		var moduleCollider = other.GetComponent<ModuleInteractCollider>();
		var airlock = other.GetComponent<Airlock>();
        var validInteraction = false;
		if (moduleCollider != null)
		{
            validInteraction = true;
			nearbyModule = null;
		}
		else if (airlock != null && airlock == nearbyAirlock)
		{
            validInteraction = true;
			nearbyAirlock = null;
		}
		else
		{
			var slot = FindModuleSlot(other);
			if (slot == nearbySlot)
			{
                validInteraction = true;
				nearbySlot = null;
			}
		}
        if (validInteraction)
        {
            var events = other.GetComponent<EventsForToggle>();
            if (events != null)
            {
                events.TriggerNegative();
            }
        }
	}

	private ModuleSlot FindModuleSlot(Collider other)
	{
		var slot = allyShip?.GetModuleSlotAtCollider(other);

		return slot;
	}

    public void TriggerAirlockAnimation(bool leaving)
    {
        if(leaving)
        {
            crunkAnimation.GoIntoSpace();
        }
        else
        {
            crunkAnimation.GoIntoShip();
        }
        crunkAnimation.TriggerAirlockAnimation();
    }

	public void PickupModule(Module module)
	{
        crunkAnimation.StartGrabbing();
		grabbedModule = module;
        grabbedModule.GetComponent<Rigidbody>().isKinematic = true;
		grabbedModule.transform.parent = moduleContainer;
		grabbedModule.transform.localPosition = Vector3.zero;

		if (grabbedModule.ModuleCollider != null)
		{
			grabbedModule.ModuleCollider.enabled = false;
			grabbedModule.transform.LookAt(grabbedModule.transform.position + -transform.forward);
		}
	}

	public void DropModule(bool isFree)
	{
		if (isFree)
		{
			if (grabbedModule.ModuleCollider != null)
			{
				grabbedModule.ModuleCollider.enabled = true;
			}
			grabbedModule.GetComponent<Rigidbody>().isKinematic = false;
		}

		crunkAnimation.StopGrabbing();
		grabbedModule.transform.parent = null;
		grabbedModule = null;
	}
}
