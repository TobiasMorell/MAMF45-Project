using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SneezeIllness : Illness {
	public abstract int GetSneezeIntervalMax ();
	public abstract int GetSneezeIntervalMin ();
}
