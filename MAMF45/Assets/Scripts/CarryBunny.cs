using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class CarryBunny : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		var interacteble = GetComponent<Interactable>();
		interacteble.onAttachedToHand += OnAttachedToHandDelegate;
		interacteble.onDetachedFromHand += OnDetachedFromHandDelegate;
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void OnAttachedToHandDelegate(Hand hand) {
		GetComponent<Animator>().SetTrigger("PickedUp");
		var grabPoint = transform.Find ("GrabPoint");
		transform.position = hand.transform.position - grabPoint.position;
		Debug.Log("Picked up!");
	}

	public void OnDetachedFromHandDelegate(Hand hand) {
		GetComponent<Animator>().SetTrigger("Dropped");
	}
}
