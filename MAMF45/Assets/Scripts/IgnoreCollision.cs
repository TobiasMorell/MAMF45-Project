using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollision : MonoBehaviour {
	public List<Collider> IgnoredColliders;

	// Use this for initialization
	void Start () {
		var ownCollider = GetComponent<Collider> ();
		foreach (var col in IgnoredColliders) {
			Physics.IgnoreCollision (ownCollider, col);
		}
	}
}
