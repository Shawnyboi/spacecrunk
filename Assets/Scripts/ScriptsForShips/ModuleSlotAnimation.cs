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
		attachAnimator.SetBool(attachFlag, false);
		attachAnimator.SetBool(transitionFlag, true);
	}

	public void FinishAttaching()
	{
		attachAnimator.SetBool(transitionFlag, false);
		attachAnimator.SetBool(attachFlag, true);

	}

	public void BeginDettaching()
	{
		attachAnimator.SetBool(attachFlag, true);
		attachAnimator.SetBool(transitionFlag, true);
	}

	public void FinishDettaching()
	{
		attachAnimator.SetBool(transitionFlag, false);
		attachAnimator.SetBool(attachFlag, false);
	}

	public void StopTransitioning()
	{
		attachAnimator.SetBool(transitionFlag, false);
	}
}
