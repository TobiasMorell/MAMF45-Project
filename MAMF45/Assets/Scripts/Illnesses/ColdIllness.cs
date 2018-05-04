using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdIllness : SneezeIllness {

	private float duration;

	void Start() {
		duration = Random.Range (Constants.Instance.TimerColdCureMin, Constants.Instance.TimerColdCureMax);
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

	public override float GetSneezeIntervalMax ()
	{
		return Constants.Instance.TimerColdSneezeMax;
	}

	public override float GetSneezeIntervalMin ()
	{
		return Constants.Instance.TimerColdSneezeMin;
	}

	public override IllnessTypes GetIllnessType ()
	{
		return IllnessTypes.Cold;
	}
}
