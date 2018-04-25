using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Illness : MonoBehaviour {
	public abstract bool Cure();
	public abstract Illness Infect (GameObject obj);
	public abstract Color GetIllnessColor();
}
