using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathIllness : Illness {
	
	public override bool Cure() {
		return false;
	}

	public override Illness Infect (GameObject obj)
	{
		return obj.AddComponent<DeathIllness> ();
	}

	public override IllnessTypes GetIllnessType ()
	{
		return IllnessTypes.Death;
	}

	public override float GetUITimerMax ()
	{
		return -1;
	}
}
