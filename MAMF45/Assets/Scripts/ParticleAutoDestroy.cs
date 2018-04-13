using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAutoDestroy : MonoBehaviour {

	void Start ()
	{
		var particles = GetComponentsInChildren<ParticleSystem> ();
		var longestDuration = 0.0f;
		foreach (var particle in particles) {
			longestDuration = Mathf.Max (longestDuration, particle.main.duration);
		}
		Destroy (gameObject, longestDuration);
	}
}
