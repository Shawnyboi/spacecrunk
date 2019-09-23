using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleAttachAudio : MonoBehaviour
{
	public AudioSource attach;
	public AudioSource detach;

	public void Attach()
	{
		attach.Play();
	}

	public void Detach()
	{
		detach.Play();
	}
}
