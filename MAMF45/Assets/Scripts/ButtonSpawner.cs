using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonSpawner : MonoBehaviour {
	public GameObject ThingToSpawn;
	public float SpawnCooldown;
	public bool StartOnCooldown = false;
	public Transform SpawnPoint;
	public Light SpawnLight;
	public float AnimationSpeed;
	public UnityEvent OnSpawn;
	public UnityEvent OnSpawnRejected;
	public UnityEvent OnCooldownEnded;

	private float CooldownCounter = 0f;
	private Coroutine ClickCoroutine;

	private Vector3 DefaultButtonPosition;

	// Use this for initialization
	void Start () {
		DefaultButtonPosition = transform.localPosition;
		SpawnLight.color = StartOnCooldown ? Color.red : Color.green;
		CooldownCounter = StartOnCooldown ? SpawnCooldown : 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (CooldownCounter > 0) {
			CooldownCounter -= Time.deltaTime;
			if (CooldownCounter <= 0) {
				SpawnLight.color = Color.green;
				CooldownCounter = 0;
				if (OnCooldownEnded != null)
					OnCooldownEnded.Invoke ();
			}
		}
	}

	public void Spawn() {
		ResetClickAnimation ();
		if (CooldownCounter > 0) {
			if (OnSpawnRejected != null) {
				OnSpawnRejected.Invoke ();
			}
			return;
		}

		var thing = Instantiate (ThingToSpawn);
		thing.transform.position = SpawnPoint.position;
		TriggerCooldown ();
		if (OnSpawn != null)
			OnSpawn.Invoke ();
	}

	private void TriggerCooldown() {
		SpawnLight.color = Color.red;
		CooldownCounter = SpawnCooldown;
	}

	private void ResetClickAnimation() {
		if (ClickCoroutine != null)
			StopCoroutine (ClickCoroutine);
		transform.localPosition = DefaultButtonPosition;
		ClickCoroutine = StartCoroutine (PlayClickAnimation());
	}

	IEnumerator PlayClickAnimation() {
		while (transform.localPosition.x >= 0.52f) {
			transform.localPosition -= new Vector3 (Time.deltaTime * AnimationSpeed, 0);
			yield return null;
		}
		while (transform.localPosition.x <= 0.58f) {
			transform.localPosition += new Vector3 (Time.deltaTime * AnimationSpeed, 0);
			yield return null;
		}
		transform.localPosition = DefaultButtonPosition;
	}

	public void OnHoverBegin() {
		GetComponentInChildren<MeshRenderer> ().material.color = CooldownCounter > 0 ? Color.red : Color.green;
	}

	public void OnHoverEnd() {
		GetComponentInChildren<MeshRenderer> ().material.color = Color.white;
	}
}
