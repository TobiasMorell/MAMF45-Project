using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;


public class ShakeMe : MonoBehaviour {

    [SerializeField]
    private float scale = 20;
    private bool isHeld = false;
    private Vector3 prevPos;
    private Vector3 prevVel;

    // Use this for initialization
    void Awake () {
        var interacteble = GetComponent<Interactable>();
        interacteble.onAttachedToHand += OnAttachedToHandDelegate;
        interacteble.onDetachedFromHand += OnDetachedFromHandDelegate;
        prevPos = transform.position;
        prevVel = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
		if (isHeld) {
			var vel = (transform.position - prevPos);
			var acc = (vel - prevVel).magnitude;
			prevPos = transform.position;
			prevVel = vel;
			transform.localScale += new Vector3 (1, 1, 1 * 0.75f) * acc * Time.deltaTime * scale;
			transform.localScale = new Vector3 (Mathf.Min (1, transform.localScale.x), Mathf.Min (1, transform.localScale.y), Mathf.Min (0.75f, transform.localScale.z));
		} else if (transform.position.y > 2) {
			transform.localScale -= new Vector3 (1, 1, 1 * 0.75f) * Time.deltaTime * scale * 0.05f;
			transform.localScale = new Vector3 (Mathf.Max (0.2f, transform.localScale.x), Mathf.Max (0.2f, transform.localScale.y), Mathf.Max (0.15f, transform.localScale.z));
		}
	}


    public void OnAttachedToHandDelegate(Hand hand) {
        isHeld = true;
    }

    public void OnDetachedFromHandDelegate(Hand hand) {
        isHeld = false;
    }


}
