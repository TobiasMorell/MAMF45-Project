using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
	private int MAXIMUM_SNEEZE_INTERVAL = 20;
	private float SNEEZE_RADIUS = 0.2f;

	public bool StartInfected = false;

	public GameObject SneezeParticleEffect;
	public GameObject SneezeHitParticleEffect;
	public GameObject SicknessCloudEffect;

	private GameObject sicknessCloudInstance = null;

	private bool isSick;
	private float sneezeTimer;

	void Start ()
	{
		if (StartInfected) {
			Infect ();
		}
	}

	void Update ()
	{
		if (isSick) {
			sneezeTimer -= Time.deltaTime;
			if (sneezeTimer < 0) {
//				StartCoroutine ("Sneeze");

				SneezeStart ();
			}
		}
	}

	public void SneezeStart() {
		sneezeTimer = 10000;
		var movement = GetComponent<BasicMovement> ();
		var animator = GetComponent<Animator> ();

		movement.Stop ();
		animator.SetTrigger ("Sneeze");
	}

	public void Sneeze() {
		var nose = GetComponentInChildren<Nose> ();
		if (!nose.IsCovered ()) {
			var nosePosition = GetNosePosition ();
			Instantiate (SneezeParticleEffect, nosePosition, transform.rotation);

			var colliders = Physics.OverlapSphere (nosePosition + transform.forward * SNEEZE_RADIUS, SNEEZE_RADIUS);
			foreach (var collider in colliders) {
				var health = collider.GetComponent<Health> ();
				if (collider.gameObject != gameObject && health != null) {
					health.Infect ();
				}
			}
		}
	}

	public void SneezeEnd() {
		var movement = GetComponent<BasicMovement> ();
		movement.Restart ();

		ResetSneezeTimer ();
	}

	private Vector3 GetNosePosition() {
		var nose = GetComponentInChildren<Nose> ();
		return nose.transform.position;
	}


	public void Infect ()
	{
		if (!isSick) {
			ResetSneezeTimer ();
			print ("New rabbit infected!");

			var sicknessProperty = GetComponentInChildren<SicknessMaterialBlockProperty> ();
			sicknessProperty.ToggleSickness (true);
			sicknessCloudInstance = Instantiate(SicknessCloudEffect, transform);
			Instantiate(SneezeHitParticleEffect, transform.position, transform.rotation);
		}
		isSick = true;
	}

	private void ResetSneezeTimer ()
	{
		sneezeTimer = Random.Range (0f, MAXIMUM_SNEEZE_INTERVAL);
	}

	public void Cure ()
	{
		if (isSick)
		{
			var sicknessProperty = GetComponentInChildren<SicknessMaterialBlockProperty>();
			sicknessProperty.ToggleSickness(false);
			if (sicknessCloudInstance != null) { 
				Destroy(sicknessCloudInstance);
				sicknessCloudInstance = null;
			}
		}
		isSick = false;
	}


	public bool IsSick ()
	{
		return isSick;
	}

	public bool IsHealthy ()
	{
		return !isSick;
	}

	void OnDrawGizmos ()
	{
		if (isSick) {
			Gizmos.color = new Color (1, 0, 0, 0.5f);
			Gizmos.DrawWireSphere (GetNosePosition() + transform.forward * SNEEZE_RADIUS, SNEEZE_RADIUS);
		}
	}
}
