using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CrunkAnimation : MonoBehaviour
{
    public bool isInSpace;
    public bool isMoving;
    public bool isHoldingSomething;
    public bool isPumping;
	public bool isBackstroking;
	private bool wasBackstroking;
    [SerializeField]
    private Animator anim;
    private states currentState = states.Idle;
    
    public void StartMoving() { isMoving = true; }
    public void StopMoving() { isMoving = false; }
    public void GoIntoSpace() { isInSpace = true; }
    public void GoIntoShip() { isInSpace = false; }
    public void StartGrabbing() { isHoldingSomething = true; }
    public void StopGrabbing() { isHoldingSomething = false; }
    public void StartPumping() { isPumping = true; }
    public void StopPumping() { isPumping = false; }
	public void StartBackstroking() { isBackstroking = true; }
	public void StopBackstroking() { isBackstroking = false; }
	public void TriggerAirlockAnimation()
    {
        if (isInSpace)
        {
            anim.SetTrigger("AirlockExit");
        }
        else
        {
            anim.SetTrigger("AirlockEnter");
        }
    }
    private states DetermineCurrentState()
    {
        if (isInSpace)
        {
            if (isHoldingSomething)
            {
                return states.Grabbing;
            }
            else if (isMoving)
            {
                return states.Swimming;
            }
            else
            {
                return states.InSpace;
            }
        }
        else
        {
            if (isPumping)
            {
                return states.Pumping;
            }
            else if (isMoving)
            {
                return states.Walking;
            }
            else
            {
                return states.Idle;
            }
        }
    }

    void ClearAnimationState()
    {
        anim.SetBool("Idle", false);
        anim.SetBool("InSpace", false);
        anim.SetBool("Walk", false);
        anim.SetBool("Pump", false);
		anim.SetBool("Swimming", false);
		anim.SetBool("Backstroke", false);
        anim.SetBool("Grab", false);
    }
    void ChangeAnimationState()
    {
        ClearAnimationState();
       switch (currentState){
            case states.Idle:
                anim.SetBool("Idle", true);
                return;
            case states.InSpace:
                anim.SetBool("InSpace", true);
                return;
            case states.Walking:
                anim.SetBool("Walk", true);
                return;
            case states.Pumping:
                anim.SetBool("Pump", true);
                return;
            case states.Swimming:
                anim.SetBool("Swimming", true);
                return;
            case states.Grabbing:
                anim.SetBool("Grab", true);
                return;

        }
    }

    void UpdateAnimator()
    {
        var newState = DetermineCurrentState();
        if (newState != currentState)
        {
            currentState = newState;
            ChangeAnimationState();
        }

		if (isBackstroking)
		{
			anim.SetBool("Backstroke", true);
		}
		else if (wasBackstroking)
		{
			anim.SetBool("Backstroke", false);
		}

		wasBackstroking = isBackstroking;
	}
    void Update()
    {
        UpdateAnimator();
    }

    private enum states
    {
        Idle,
        Walking,
        Pumping,
        InSpace,
        Grabbing,
        Swimming
    }
}
