using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class CarryBunny : MonoBehaviour {
    private float originalAngle;
    private Vector3 carryOffset;
    private bool isGrabbed;

    void Awake () {
		var interacteble = GetComponent<Interactable>();
		interacteble.onAttachedToHand += OnAttachedToHandDelegate;
		interacteble.onDetachedFromHand += OnDetachedFromHandDelegate;
	}
    
    void Update() {
        var euler = transform.rotation.eulerAngles;
        if (isGrabbed) {
            transform.position = transform.parent.transform.position + Quaternion.Euler(0, transform.parent.rotation.eulerAngles.y - originalAngle, 0) * carryOffset;
        }
        transform.rotation = Quaternion.Euler(0, euler.y, 0);
    }

    public void OnAttachedToHandDelegate(Hand hand) {
        carryOffset = transform.position - transform.parent.position;
        originalAngle = transform.parent.rotation.eulerAngles.y;
        isGrabbed = true;
    }

	public void OnDetachedFromHandDelegate(Hand hand) {
        isGrabbed = false;
    }
}
