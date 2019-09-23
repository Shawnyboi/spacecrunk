using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleCharge : MonoBehaviour
{
	public GameObject meter = null;

	public float empty;
	public float full;

    public void ShowCharge(float charge)
	{
		float pos = (empty * (1 - charge)) + (full * charge);
		meter.transform.position = new Vector3(meter.transform.position.x, pos, meter.transform.position.z);
	}
}
