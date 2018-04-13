using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Syringe : MonoBehaviour {
	private bool _used = false;
	public float AnimationSpeed;
	private Joint pistonContainerJoint;
	public Transform Piston;
    public Material ValidPistonMaterial;
    public Material InvalidPistonMaterial;

	public Health Target {
		private get;
		set;
	}

    public void OnPistonHover() {
        if (Target != null)
            Piston.GetComponent<MeshRenderer>().material = ValidPistonMaterial;
        else
            Piston.GetComponent<MeshRenderer>().material = InvalidPistonMaterial;
    }

	public void Apply() {
		if (_used || Target == null)
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
        Debug.Log(Piston.localPosition);
        while (Mathf.Abs(Piston.localPosition.y) < 1.8) {
			Piston.localPosition -= new Vector3(0, AnimationSpeed * Time.deltaTime, 0);
			yield return null;
		}
		Piston.localPosition = new Vector3(0, -1.8f, 0);
		UnlockPositionAndEnableJoint ();
        Destroy(Piston.GetComponent<Interactable>());

		Target.Cure ();
	}
}
