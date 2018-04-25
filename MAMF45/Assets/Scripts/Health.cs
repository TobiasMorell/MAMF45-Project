using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
	private int MAXIMUM_SNEEZE_INTERVAL = 20;
	private int SNEEZE_FORCE = 1000;

	public bool StartInfected = false;

	public GameObject SneezeParticleEffect;
	public GameObject SneezeHitParticleEffect;
	public GameObject SicknessCloudEffect;

	private GameObject sicknessCloudInstance = null;

    private bool isSick;
	private float sneezeTimer;

    private Animator animator;
    private BasicMovement movement;

    private bool isProtected;
    private GameObject contraceptive = null;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<BasicMovement>();
    }

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

		movement.Stop ();
		animator.SetTrigger ("Sneeze");
	}

	public void Sneeze() {
		var nose = GetComponentInChildren<Nose> ();
		nose.Sneeze (SneezeParticleEffect);
        if (contraceptive != null) {
			contraceptive.GetComponent<Rigidbody>().AddForce(Vector3.up * SNEEZE_FORCE);
        }
	}

	public void SneezeEnd() {
		movement.Restart ();
		ResetSneezeTimer ();
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

            animator.SetFloat("SicknessBlend", 1.0f);
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
            animator.SetFloat("SicknessBlend", 0f);
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


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ContraceptiveTrigger"))
        {
            isProtected = true;
            contraceptive = other.transform.parent.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ContraceptiveTrigger"))
        {
            isProtected = false;
            contraceptive = null;
        }
    }
}
