using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;


public class ShakeMe : MonoBehaviour {

    [SerializeField]
    private float scale = 10;
    private bool isHeld = false;
    private Vector3 prevPos;

    // Use this for initialization
    void Awake () {
        var interacteble = GetComponent<Interactable>();
        interacteble.onAttachedToHand += OnAttachedToHandDelegate;
        interacteble.onDetachedFromHand += OnDetachedFromHandDelegate;
        prevPos = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        var vel = (transform.position - prevPos).magnitude;
        prevPos = transform.position;
        transform.localScale += new Vector3(1,1,1*0.75f) * vel * Time.deltaTime * scale;
        Debug.Log("Vel: " + vel);
        transform.localScale = new Vector3(Mathf.Min(1, transform.localScale.x), Mathf.Min(1, transform.localScale.y), Mathf.Min(0.75f, transform.localScale.z));
	}


    public void OnAttachedToHandDelegate(Hand hand) {
        isHeld = true;
    }

    public void OnDetachedFromHandDelegate(Hand hand) {
        isHeld = false;
    }


}
