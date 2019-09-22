using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventsForToggle : MonoBehaviour
{
	[SerializeField]
	UnityEvent onPositive = null;
	[SerializeField]
	UnityEvent onNegative = null;

	public void TriggerPositive()
	{
		onPositive.Invoke();
	}

	public void TriggerNegative()
	{
		onNegative.Invoke();
	}
}
