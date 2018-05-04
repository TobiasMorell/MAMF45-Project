using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timers : MonoBehaviour {
	public static Timers Instance {
		get;
		private set;
	}

	public void Awake() {
		if (Instance != null)
			Debug.LogError ("More than one 'Timers' exists in the scene. Please make sure only to place one at a time!");
		Instance = this;
	}

	public float SpawnSyringe = 15f;
	public float SpawnNapkin = 1f;
	public float SpawnContraceptive = 10f;
	public float CureCold = 60f;
	public float PneumoniaDeath = 50f;
	public float MakeLove = 20f;
	public float Sneeze = 15f;
	public float TriggerHealthyBunny = 60f;
}
