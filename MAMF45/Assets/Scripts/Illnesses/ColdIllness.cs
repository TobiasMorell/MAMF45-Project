﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdIllness : SneezeIllness {

	private float duration;

	void Start() {
		duration = Random.Range (25, 35);
	}

	void Update() {
		duration -= Time.deltaTime;
		if (duration < 0) {
			GetComponent<Health> ().Cure (this);
		}
	}
	
	public override bool Cure() {
		return false;
	}

	public override Illness Infect (GameObject obj)
	{
		return obj.AddComponent<ColdIllness> ();
	}

	public override int GetSneezeIntervalMax ()
	{
		return 20;
	}

	public override int GetSneezeIntervalMin ()
	{
		return 10;
	}

	public override IllnessTypes GetIllnessType ()
	{
		return IllnessTypes.Cold;
	}
}
