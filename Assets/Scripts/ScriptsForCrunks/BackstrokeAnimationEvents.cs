using System;
using UnityEngine;

public class BackstrokeAnimationEvents : MonoBehaviour
{
	public void ReachedPeak()
	{
		GetComponentInParent<CrunkBackstroke>().ReachedPeak();
	}
}