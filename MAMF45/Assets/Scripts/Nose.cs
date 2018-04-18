using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nose : MonoBehaviour {
	private float SNEEZE_RADIUS = 0.2f;

	private List<Napkin> currentNapkins = new List<Napkin>();

	public bool IsCovered() {
		return currentNapkins.Count > 0;
	}

	void OnTriggerEnter(Collider collider) {
		var napkin = collider.gameObject.GetComponent<Napkin> ();
		if (napkin) {
			currentNapkins.Add (napkin);
			if (napkin.SpreadsDisease()) {
				var health = GetComponentInParent<Health> ();
				health.Infect ();
			}
		}
	}

	void OnTriggerExit(Collider collider) {
		var napkin = collider.gameObject.GetComponent<Napkin> ();
		currentNapkins.Remove (napkin);
	}

	public void Sneeze(GameObject sneezeEffect) {
		if (!IsCovered ()) {
			Instantiate (sneezeEffect, transform.position, transform.rotation);

			var colliders = Physics.OverlapSphere (transform.position + transform.forward * SNEEZE_RADIUS, SNEEZE_RADIUS);
			foreach (var collider in colliders) {
				var health = collider.GetComponent<Health> ();
				if (collider.gameObject != gameObject && health != null) {
					health.Infect ();
				}
			}
		} else {
			foreach (var napkin in currentNapkins) {
				napkin.Use ();
			}
		}
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
