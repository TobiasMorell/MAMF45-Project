using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonTrigger : MonoBehaviour {
	public UnityEvent OnButtonTriggered;

	void OnTriggerEnter(Collider other) {
		if (OnButtonTriggered != null && other.CompareTag(Tags.BUTTON))
			OnButtonTriggered.Invoke ();
	}
}
