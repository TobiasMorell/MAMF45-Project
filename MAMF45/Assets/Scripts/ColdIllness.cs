using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdIllness : SneezeIllness {
	
	public override bool Cure() {
		return false;
	}

	public override Illness Infect (GameObject obj) {
		return obj.AddComponent<ColdIllness> ();
	}

	public override Color GetIllnessColor() {
		return new Color (41/255f, 120/255f, 41/255f, 1f);
	}
}
