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

		if (DifficultyLevel > 1)
		{
			MaxBunnyCount += DifficultyLevel;
			SpawnRate -= DifficultyLevel;
			SwarmRate -= DifficultyLevel * 10;
			TimerColdSneezeMin -= DifficultyLevel;
			TimerColdSneezeMax -= DifficultyLevel;
			TimerPneumoniaSneezeMin -= DifficultyLevel;
			TimerPneumoniaSneezeMax -= DifficultyLevel;
			TimerPneumoniaDeathMin -= DifficultyLevel;
			TimerLoveIntervals -= DifficultyLevel*2;
			TimerLoveReactionTime -= DifficultyLevel;
		}
	}

	[Header("General")]
	public bool HasGameBegun = false;
	public int DifficultyLevel = 1;

	[Header("Bunny spawn")]
	public int MaxBunnyCount = 8;
	public int SpawnRate = 10;
	public int SwarmRate = 120;

	[Header("Cold Timers")]
	public float TimerColdSneezeMax = 20f;
	public float TimerColdSneezeMin = 10f;
	public float TimerColdCureMax = 35f;
	public float TimerColdCureMin = 25f;

	[Header("Pneumonia Timers")]
	public float TimerPneumoniaSneezeMax = 30f;
	public float TimerPneumoniaSneezeMin = 15;
	public float TimerPneumoniaDeathMax = 60f;
	public float TimerPneumoniaDeathMin = 50f;

	[Header("Love Timers")]
	public float TimerLoveIntervals = 20f;
	public float TimerLoveReactionTime = 5f;

	[Header("Misc Timers")]
	public float TimerTriggerHealthyBunny = 20f;
	public float BunnyDespawnDelay = 75f;
	public float GameTime = 150f;
	public float GameEndFadeTime = 20f;

	[Header("Scores")]
	public int ScoreBunnyDied = -50;
	public int ScoreBunnyHeartSaved = 30;
	public int ScoreBonusHole = 20;
	public int ScoreRecycle = 5;
	public int ScoreBunnyNoHeartSaved = 10;
	public int ScoreBunnyKilledByPlayer = -100;
	public int ScoreSneezePrevented = 15;

	[Header("Diease Changes")]
	public float ChanceCold = 0.50f;
	public float ChancePneunemia = 0.15f;
	public float ChanceChlamydia = 0.20f;
	public float RareDiseaseScalingFactor = 0.05f;
}
