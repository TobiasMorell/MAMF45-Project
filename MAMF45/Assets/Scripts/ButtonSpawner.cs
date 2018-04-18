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

	private float _cooldownCounter = 0f;
	private Coroutine _clickCoroutine;
	private bool _hovered;

	private Vector3 _defaultButtonPosition;

	// Use this for initialization
	void Start () {
		_defaultButtonPosition = transform.localPosition;
		SpawnLight.color = StartOnCooldown ? Color.red : Color.green;
		_cooldownCounter = StartOnCooldown ? SpawnCooldown : 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (_cooldownCounter > 0) {
			_cooldownCounter -= Time.deltaTime;
			if (_cooldownCounter <= 0) {
				SpawnLight.color = Color.green;
				_cooldownCounter = 0;
				if (OnCooldownEnded != null)
					OnCooldownEnded.Invoke ();
			}
		}
	}

	public void Spawn() {
		if (_cooldownCounter > 0) {
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
		_cooldownCounter = SpawnCooldown;
	}
}
