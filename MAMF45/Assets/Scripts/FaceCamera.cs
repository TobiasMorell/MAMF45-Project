using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour {
	
	void Update () {
		Camera cam = Camera.main;
		transform.LookAt(cam.transform.position, Vector3.up);
		transform.Rotate (0, 180, 0);
	}
}
