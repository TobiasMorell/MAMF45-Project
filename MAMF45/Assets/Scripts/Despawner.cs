using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Despawner : MonoBehaviour {

	private const float TIME_BEFORE_SINKING = 10;
	private const float SINKING_SPEED = 0.1f;

	private float timer;
	
	void Start () {
		GetComponentInChildren<BasicMovement> ().enabled = false;
		GetComponentInChildren<Nose>().Disable();
		GetComponentInChildren<Rigidbody> ().isKinematic = true;
		Destroy (GetComponent<Throwable>());
	}

	public void ToggleDeath() {
		GameObject.Find("TombstoneSpawnPoint").GetComponent<TombstoneSpawner>().SpawnTombstone();
	}

	void Update () {
		timer += Time.deltaTime;
		if (timer > TIME_BEFORE_SINKING) {
			transform.position = transform.position - Vector3.up * Time.deltaTime * SINKING_SPEED;
		}

		if (transform.position.y < -1) {
			Destroy (gameObject);
		}
	}
}
