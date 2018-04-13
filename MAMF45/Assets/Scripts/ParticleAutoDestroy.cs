using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAutoDestroy : MonoBehaviour {

	void Start ()
	{
		var particle = GetComponent<ParticleSystem> ();
		Destroy (gameObject, particle.main.duration);
	}
}
