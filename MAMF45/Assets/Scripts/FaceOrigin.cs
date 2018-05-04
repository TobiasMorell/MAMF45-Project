using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceOrigin : MonoBehaviour {

	void Start () {
		transform.rotation = Quaternion.LookRotation(transform.position, Vector3.up);
	}
}
