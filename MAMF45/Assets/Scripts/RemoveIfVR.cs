using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveIfVR : MonoBehaviour {
	
	void Start () {
		if (GameObject.Find("Player") != null) {
			gameObject.SetActive(false);
		}
	}
}
