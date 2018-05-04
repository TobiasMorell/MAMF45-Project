using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SneezeIllness : Illness {
	public abstract float GetSneezeIntervalMax ();
	public abstract float GetSneezeIntervalMin ();
}
