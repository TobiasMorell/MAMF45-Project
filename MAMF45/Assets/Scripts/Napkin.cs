using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Napkin : MonoBehaviour {
	private int useCount;

	public void Use() {
		useCount += 1;
	}
}
