using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {

	private HashSet<Destructible> recycled;

	void Start() {
		recycled = new HashSet<Destructible> ();
	}

	void OnTriggerEnter(Collider collider) {
		var destructible = collider.GetComponentInChildren<Destructible> ();
		if (!destructible)
			destructible = collider.GetComponentInParent<Destructible> ();
		if (destructible && !destructible.IsHeld()) {
			Destroy (destructible.gameObject, 5.0f);
			if (!recycled.Contains (destructible)) {
				ScoreBoard.Instance.GivePoints (Constants.Instance.ScoreRecycle);
				recycled.Add(destructible);
			}
		}
	}
}
