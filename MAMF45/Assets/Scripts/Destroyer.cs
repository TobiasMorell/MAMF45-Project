using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {

	void OnTriggerEnter(Collider collider) {
		var destructible = collider.GetComponentInChildren<Destructible> ();
		if (!destructible)
			destructible = collider.GetComponentInParent<Destructible> ();
		if (destructible && !destructible.IsHeld()) {
			Destroy (destructible.gameObject);
		}
	}
}
