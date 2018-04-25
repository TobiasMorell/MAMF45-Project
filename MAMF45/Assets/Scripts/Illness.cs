using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Illness : MonoBehaviour {
	public abstract bool Cure();
	public abstract Illness Infect (GameObject obj);
	public abstract IllnessTypes GetType();

	public override bool Equals (object other)
	{
		return other.GetType() == GetType();
	}

	public override int GetHashCode()
	{
		return GetType ().GetHashCode ();
	}
}
