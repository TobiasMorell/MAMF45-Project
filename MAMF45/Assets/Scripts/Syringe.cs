using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Syringe : MonoBehaviour {
	private bool _used= false;
	public float AnimationSpeed;
	private Joint pistonContainerJoint;
	public Transform Piston;

	public Health Target {
		private get;
		set;
	}

	public void Apply() {
		if (_used /*|| Target == null*/)
			return;
		_used = true;

		LockPositionAndDisableJoint ();
		StartCoroutine (PressDown ());
	}

	void LockPositionAndDisableJoint() {
		Destroy(GetComponentInChildren<Joint> ());
		var rigidbodies = GetComponentsInChildren<Rigidbody> ();
		foreach (var rb in rigidbodies) {
			if (rb.name != "Container")
				continue;
			rb.constraints = RigidbodyConstraints.FreezeAll;
			rb.GetComponent<Collider> ().enabled = false;
			break;
		}
	}

	void UnlockPositionAndEnableJoint() {
		var rigidbodies = GetComponentsInChildren<Rigidbody> ();
		Rigidbody pistonRb = null;
		Joint joint = null;
		foreach (var rb in rigidbodies) {
			Debug.Log (rb.name);
			if (rb.name == "Container") {
				joint = rb.gameObject.AddComponent<FixedJoint> ();
				rb.constraints = RigidbodyConstraints.None;
				rb.GetComponent<Collider> ().enabled = true;
			} else
				pistonRb = rb;
		}
		joint.connectedBody = pistonRb;
	}

	IEnumerator PressDown() {
		while (Piston.localPosition.y > 0) {
			Piston.localPosition -= new Vector3(0, AnimationSpeed * Time.deltaTime);
			yield return null;
		}
		Piston.localPosition = new Vector3(0, 0);
		UnlockPositionAndEnableJoint ();
		//Target.Cure ();
	}
}
