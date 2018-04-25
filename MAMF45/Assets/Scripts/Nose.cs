using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nose : MonoBehaviour {
	private const int MAXIMUM_SNEEZE_INTERVAL = 20;
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
			ResetSneezeTimer ();
		}
	}

	public void SneezeStart() {
		sneezeTimer = 10000;

		movement.Stop ();
		animator.SetTrigger ("Sneeze");
	}

	public void Sneeze() {
		var nose = GetComponentInChildren<Nose> ();
		var contraceptive = GetComponentInParent<Health> ().GetContraceptive ();

		if (!IsCovered ()) {
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
			foreach (var napkin in currentNapkins) {
				napkin.Use (GetComponentsInParent<SneezeIllness>());
			}
		}
	}

	public void SneezeEnd() {
		movement.Restart ();
		ResetSneezeTimer ();
	}

	private void ResetSneezeTimer ()
	{
		sneezeTimer = Random.Range (0f, MAXIMUM_SNEEZE_INTERVAL);
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
