using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PneumoniaIllness : SneezeIllness {
	private const float TIME_BEFORE_DEATH = 90;

	private float sicknessTimer;

	void Update()
	{
		sicknessTimer += Time.deltaTime;
		if (sicknessTimer > TIME_BEFORE_DEATH) {
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

	public override int GetSneezeIntervalMax ()
	{
		return 60;
	}

	public override int GetSneezeIntervalMin ()
	{
		return 40;
	}

	public override IllnessTypes GetIllnessType ()
	{
		return IllnessTypes.Pneumenia;
	}
}
