using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeRotation : MonoBehaviour {

	private Quaternion rotation;

	void Start () {
		rotation = transform.rotation;
	}

	void LateUpdate () {
		transform.rotation = rotation;
	}
}
