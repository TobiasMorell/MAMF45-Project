using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyOffScreen : MonoBehaviour {
	private const int FORCE = 2;

	private Vector3 direction;
	private Rigidbody body;

	void Start () {
		var dir = Random.Range(0, Mathf.PI * 2);
		direction = new Vector3(Mathf.Cos(dir), 0, Mathf.Sin(dir)) * FORCE;

		body = GetComponent<Rigidbody>();
	}

	void Update () {
		if (transform.position.y > 2)
			body.AddForce(direction);
	}
}
