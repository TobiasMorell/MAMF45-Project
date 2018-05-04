using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToEdge : MonoBehaviour {
	private Transform _target;

	// Use this for initialization
	void Start () {
		var bm = GetComponent<BasicMovement> ();
		if (bm)
			Destroy (bm);


	}

	void Update() {

	}
}
