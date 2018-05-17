using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Valve.VR.InteractionSystem;

public class ResetPosition : MonoBehaviour {
	private const float HOLD_LIMIT = 3;

	private float held;
	private Hand h1, h2;
	private Player player;

	// Use this for initialization
	void Start () {
		h1 = GetComponentsInChildren<Hand>()[0];
		h2 = GetComponentsInChildren<Hand>()[1];
		player = GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		if (h1.GetTouchPadButton() && h2.GetTouchPadButton())
			held += Time.deltaTime;
		else
			held = 0;
		if (held >= HOLD_LIMIT) {
			held = float.NegativeInfinity;
			Vector3 playerFeetOffset = player.trackingOriginTransform.position - player.feetPositionGuess;
			player.trackingOriginTransform.position = playerFeetOffset;
			Debug.Log ("Position reset");

            GameObject.FindGameObjectWithTag(Tags.GAME_CONTROLLER).GetComponent<GameStarter>().StartGame();
		}
	}
}
