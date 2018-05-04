using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClamydiaIllness : Illness {
	
	public override bool Cure() {
		return true;
	}

	public override Illness Infect (GameObject obj)
	{
		return obj.AddComponent<ClamydiaIllness> ();
	}

	public override IllnessTypes GetIllnessType ()
	{
		return IllnessTypes.Clamydia;
	}
}
