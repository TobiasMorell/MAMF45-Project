using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PneumoniaIllness : SneezeIllness {
	private float sicknessTimer;

	void Start()
	{
		sicknessTimer = Random.Range (Constants.Instance.TimerPneumoniaDeathMin, Constants.Instance.TimerPneumoniaDeathMax);
	}

	void Update()
	{
		sicknessTimer -= Time.deltaTime;
		if (sicknessTimer < 0) {
			GetComponent<Health> ().Die ();
		}
	}

	public override bool Cure() 
	{
		return true;
	}

	public override Illness Infect (GameObject obj)
	{
		return obj.AddComponent<PneumoniaIllness> ();
	}

	public override float GetSneezeIntervalMax ()
	{
		return Constants.Instance.TimerPneumoniaSneezeMax;
	}

	public override float GetSneezeIntervalMin ()
	{
		return Constants.Instance.TimerPneumoniaSneezeMin;
	}

	public override IllnessTypes GetIllnessType ()
	{
		return IllnessTypes.Pneumenia;
	}
}
