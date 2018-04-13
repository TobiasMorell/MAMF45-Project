using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nose : MonoBehaviour {

	private bool isCovered;

	public bool IsCovered() {
		return isCovered;
	}

	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.GetComponent<Napkin> ()) {
			isCovered = true;
		}
	}

	void OnTriggerExit(Collider collider) {
		if (collider.gameObject.GetComponent<Napkin> ()) {
			isCovered = false;
		}
	}
}
