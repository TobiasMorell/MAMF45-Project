using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour {
	public static Constants Instance {
		get;
		private set;
	}

	public void Awake() {
		if (Instance != null)
			Debug.LogError ("More than one 'Timers' exists in the scene. Please make sure only to place one at a time!");
		Instance = this;
	}

	[Header("Cold Timers")]
	public float TimerColdSneezeMax = 20f;
	public float TimerColdSneezeMin = 10f;
	public float TimerColdCureMax = 35f;
	public float TimerColdCureMin = 25f;

	[Header("Pneumonia Timers")]
	public float TimerPneumoniaSneezeMax = 60f;
	public float TimerPneumoniaSneezeMin = 40f;
	public float TimerPneumoniaDeathMax = 100f;
	public float TimerPneumoniaDeathMin = 80f;

	[Header("Love Timers")]
	public float TimerLoveIntervals = 30f;
	public float TimerLoveReactionTime = 15f;

	[Header("Misc Timers")]
	public float TimerTriggerHealthyBunny = 60f;

	[Header("Scores")]
	public int ScoreBunnyDied = -50;
	public int ScoreBunnySaved = 10;
	public int ScoreBonusHole = 20;
}
