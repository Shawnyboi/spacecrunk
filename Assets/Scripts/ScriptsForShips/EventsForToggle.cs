using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventsForToggle : MonoBehaviour
{
	public UnityEvent onPositive = null;
	public UnityEvent onNegative = null;

	public void TriggerPositive()
	{
		onPositive.Invoke();
	}

	public void TriggerNegative()
	{
		onNegative.Invoke();
	}
}
