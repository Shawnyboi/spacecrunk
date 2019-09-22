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
		Debug.Log("Attaching");
	}

	public void FinishAttaching()
	{
		attachAnimator.SetBool(transitionFlag, false);
		attachAnimator.SetBool(attachFlag, true);
		Debug.Log("Attached");

	}

	public void BeginDettaching()
	{
		Debug.Log("Dettaching");

		attachAnimator.SetBool(attachFlag, true);
		attachAnimator.SetBool(transitionFlag, true);
	}

	public void FinishDettaching()
	{
		Debug.Log("Dettached");

		attachAnimator.SetBool(transitionFlag, false);
		attachAnimator.SetBool(attachFlag, false);
	}

	public void StopTransitioning()
	{
		attachAnimator.SetBool(transitionFlag, false);
	}
}
