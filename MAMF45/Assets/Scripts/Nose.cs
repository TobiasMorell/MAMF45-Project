using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Nose : MonoBehaviour {
	private const float SNEEZE_RADIUS = 0.2f;
	private const int SNEEZE_FORCE = 1000;

	private float sneezeTimer;
	public GameObject SneezeEffect;

	private Animator animator;
	private BasicMovement movement;

	private List<Napkin> currentNapkins = new List<Napkin>();


	void Awake()
	{
		animator = GetComponentInParent<Animator>();
		movement = GetComponentInParent<BasicMovement>();
	}

	void Update() {
		if (GetComponentInParent<SneezeIllness> ()) {
			sneezeTimer -= Time.deltaTime;
			if (sneezeTimer < 0) {
				SneezeStart ();
			}
		} else {
			sneezeTimer = 10000;

			var camera = Camera.main;
			camera.GetComponent<SlowMotion>().StopSlowMotion(transform.parent.gameObject);
		}
	}

	public void SneezeStart() {
		sneezeTimer = 10000;

		movement.Stop ();
		animator.SetTrigger ("Sneeze");

		var camera = GameObject.FindGameObjectWithTag (Tags.MAIN_CAMERA);
		camera.GetComponent<SlowMotion> ().StartSlowMotion (transform.parent.gameObject);
	}

	public void Sneeze() {
		var contraceptive = GetComponentInParent<Health> ().GetContraceptive ();

		if (!IsCovered () && GetComponentInParent<SneezeIllness> ()) {
			Instantiate (SneezeEffect, transform.position, transform.rotation);

			if (!contraceptive) {
				var colliders = Physics.OverlapSphere (transform.position + transform.forward * SNEEZE_RADIUS, SNEEZE_RADIUS);
				foreach (var collider in colliders) {
					var health = collider.GetComponent<Health> ();
					if (collider.gameObject != gameObject && health != null) {
						health.Infect (GetComponentsInParent<SneezeIllness>());
					}
				}
			} else {
				contraceptive.GetComponent<Rigidbody>().AddForce(Vector3.up * SNEEZE_FORCE);
			}
		} else {
			ScoreBoard.Instance.SneezePrevented (GetComponentInChildren<Text>());
			foreach (var napkin in currentNapkins) {
				napkin.Use (GetComponentsInParent<SneezeIllness>());
			}
		}

		var camera = Camera.main;
		camera.GetComponent<SlowMotion> ().StopSlowMotion (transform.parent.gameObject);
	}

	public void SneezeEnd() {
		movement.Restart ();
		ResetSneezeTimer ();
	}

	public void ResetSneezeTimer ()
	{
		float upper = 100000;
		float lower = 100000;
		foreach (var illness in GetComponentsInParent<SneezeIllness> ()) {
			upper = Mathf.Min (upper, illness.GetSneezeIntervalMax ());
			lower = Mathf.Min (lower, illness.GetSneezeIntervalMin ());
		}

		sneezeTimer = Random.Range (lower, upper);
	}

	public void Disable() {
		var camera = Camera.main;
		camera.GetComponent<SlowMotion>().StopSlowMotion(transform.parent.gameObject);
		enabled = false;
	}


	public bool IsCovered() {
		return currentNapkins.Count > 0;
	}


	void OnTriggerEnter(Collider collider) {
		var napkin = collider.gameObject.GetComponent<Napkin> ();
		if (napkin) {
			currentNapkins.Add (napkin);
			var diseases = napkin.SpreadsDisease ();
			if (diseases.Length > 0) {
				var health = GetComponentInParent<Health> ();
				health.Infect (diseases);
			}
		}
	}

	void OnTriggerExit(Collider collider) {
		var napkin = collider.gameObject.GetComponent<Napkin> ();
		currentNapkins.Remove (napkin);
	}

	void OnDrawGizmos ()
	{
		if (IsCovered())
			Gizmos.color = new Color (0, 1, 0, 0.5f);
		else
			Gizmos.color = new Color (1, 0, 0, 0.5f);
		Gizmos.DrawWireSphere (transform.position + transform.forward * SNEEZE_RADIUS, SNEEZE_RADIUS);
	}
}
