using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Valve.VR.InteractionSystem;

public class Destructible : MonoBehaviour {

	private bool isHeld;

	// Use this for initialization
	void Start () {
		var interactible = GetComponentInChildren<Interactable> ();
		interactible.onAttachedToHand += OnAttachedToHandDelegate;
		interactible.onDetachedFromHand += OnDetachedFromHandDelegate;
	}
	
	public bool IsHeld() {
		return isHeld;
	}

	void OnAttachedToHandDelegate( Hand hand ) {
		isHeld = true;
	}
	void OnDetachedFromHandDelegate( Hand hand ) {
		isHeld = false;
	}
}
