using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyringeNeedle : MonoBehaviour {
	private Syringe syringe;

	void Start() {
		this.syringe = GetComponentInParent<Syringe> ();
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag (Tags.BUNNY)) {
			syringe.Target = other.GetComponent<Health>();
		}
	}

    private void OnTriggerExit(Collider other) {
        syringe.Target = null;
    }
}
