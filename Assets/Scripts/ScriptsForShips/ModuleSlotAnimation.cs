using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleSlotAnimation : MonoBehaviour
{
	public Animator attachAnimator = null;
	public string attachFlag = "Attached";
	public string transitionFlag = "Transitioning";

	public void BeginAttaching()
	{
		if (attachAnimator.isActiveAndEnabled)
		{
			attachAnimator.SetBool(attachFlag, false);
			attachAnimator.SetBool(transitionFlag, true);
		}
	}

	public void FinishAttaching()
	{
		if (attachAnimator.isActiveAndEnabled)
		{
			attachAnimator.SetBool(transitionFlag, false);
			attachAnimator.SetBool(attachFlag, true);
		}

	}

	public void BeginDettaching()
	{
		if (attachAnimator.isActiveAndEnabled)
		{
			attachAnimator.SetBool(attachFlag, true);
			attachAnimator.SetBool(transitionFlag, true);
		}
	}

	public void FinishDettaching()
	{
		if (attachAnimator.isActiveAndEnabled)
		{
			attachAnimator.SetBool(transitionFlag, false);
			attachAnimator.SetBool(attachFlag, false);
		}
	}

	public void StopTransitioning()
	{
		if (attachAnimator.isActiveAndEnabled)
		{
			attachAnimator.SetBool(transitionFlag, false);
		}
	}
}
