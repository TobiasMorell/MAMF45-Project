using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Syringe : MonoBehaviour {
	private bool _used = false;
    private bool _pickedUp = false;
	public float AnimationSpeed;
	public Transform Piston;
    public Material ValidPistonMaterial;
    public Material InvalidPistonMaterial;

    public GameObject container;

    public Health Target {
        private get;
        set;
    }

    private void Update() {
        if(_pickedUp) {
            if (Target != null && !_used)
                Piston.GetComponent<MeshRenderer>().material = ValidPistonMaterial;
            else
                Piston.GetComponent<MeshRenderer>().material = InvalidPistonMaterial;
        }
    }

    public void OnPickedUp() {
        _pickedUp = true;
    }
    public void OnDropped() {
        _pickedUp = false;
    }

	public void Apply() {
        Debug.Log("Apply");
		if (_used || Target == null)
			return;
		_used = true;
        Debug.Log("Curing bunny");

		LockPositionAndDisableJoint ();
		StartCoroutine (PressDown ());
	}

	void LockPositionAndDisableJoint() {
        container.GetComponents<Collider>().ForEach(c => c.enabled = false);
	}

	void UnlockPositionAndEnableJoint() {
        container.GetComponents<Collider>().ForEach(c => c.enabled = false);
	}

	IEnumerator PressDown() {
        while (Piston.localPosition.y > Mathf.Epsilon) {
            Piston.localPosition -= new Vector3(0, AnimationSpeed * Time.deltaTime);
            yield return null;
		}
		UnlockPositionAndEnableJoint ();
        Destroy(Piston.GetComponent<InteractableHoverEvents>());
        Destroy(Piston.GetComponent<InteractableButtonEvents>());
        Destroy(Piston.GetComponent<Interactable>());

		//Target.Cure ();
	}
}
