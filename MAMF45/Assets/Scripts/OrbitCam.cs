using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCam : MonoBehaviour {

	[SerializeField]
	private bool isPerspective = true;
	[SerializeField]
	private Vector3 target;
	[SerializeField]
	private GameObject spawn;
	private Camera cam;
	private Vector3 distance;
	
	void Start () {
		cam = GetComponent<Camera>();
		distance = new Vector3(0f, 0f, (transform.position - target).magnitude);
	}
	
	void Update ()
	{
		transform.Rotate(0f, -Input.GetAxis("Horizontal"), 0f, Space.World);
		transform.Rotate(Input.GetAxis("Vertical"), 0f, 0f, Space.Self);
		transform.position = target - transform.rotation * distance;

		if (Input.GetKeyDown("p"))
		{
			isPerspective = !isPerspective;
			cam.orthographic = !isPerspective;
		}

		if (Input.GetKeyDown("b"))
		{
			var clone = Instantiate(spawn, transform.position, Quaternion.Euler(0, transform.rotation.eulerAngles[1], 0));
			clone.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * 10f);
		}

		distance.z -= Input.GetAxis("Mouse ScrollWheel") * 5f;
	}
}
