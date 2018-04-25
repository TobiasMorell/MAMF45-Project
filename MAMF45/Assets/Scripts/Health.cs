using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
	public bool StartInfected = false;

	public GameObject SneezeParticleEffect;
	public GameObject SneezeHitParticleEffect;
	public GameObject SicknessCloudEffect;

	private GameObject sicknessCloudInstance = null;

	private List<Illness> illnesses;
	private float sneezeTimer;

	private bool isProtected;
	private GameObject contraceptive = null;

    private Animator animator;
    private BasicMovement movement;

    private void Awake()
    {
		illnesses = new List<Illness> ();
        animator = GetComponent<Animator>();
        movement = GetComponent<BasicMovement>();
    }

    void Start ()
	{
		if (StartInfected) {
			Infect (new ColdIllness());
		}
	}

	void Update ()
	{
	}


	public void Infect (Illness illness)
	{
		Infect(new Illness[]{illness});
	}

	public void Infect (Illness[] illnesses)
	{
		foreach (var illness in illnesses) {
			if (this.illnesses.Contains(illness)) {
				print ("Already has illness: " + illness);
			} else {
				this.illnesses.Add(illness.Infect (gameObject));
				print ("New infection!");

				// TODO Change color in some way?
				var sicknessProperty = GetComponentInChildren<SicknessMaterialBlockProperty> ();
				sicknessProperty.ToggleSickness (true);
				sicknessCloudInstance = Instantiate (SicknessCloudEffect, transform);
				Instantiate (SneezeHitParticleEffect, transform.position, transform.rotation);

				animator.SetFloat ("SicknessBlend", 1.0f);
			}
		}
	}

	public void Cure ()
	{
		foreach (var illness in illnesses)
		{
			if (illness.Cure ()) {
				Destroy (illness);

				// TODO Undo coloring?
				var sicknessProperty = GetComponentInChildren<SicknessMaterialBlockProperty> ();
				sicknessProperty.ToggleSickness (false);
				if (sicknessCloudInstance != null) { 
					Destroy (sicknessCloudInstance);
					sicknessCloudInstance = null;
				}
				animator.SetFloat ("SicknessBlend", 0f);
			} else {
				print ("Uncurable illness!");
			}
        }
	}


	public bool IsSick ()
	{
		return illnesses.Count > 0;
	}

	public bool IsHealthy ()
	{
		return !IsSick();
	}

	public GameObject GetContraceptive()
	{
		return contraceptive;
	}


	public void Sneeze() // Needed to broadcast to Nose
	{
		GetComponentInChildren<Nose> ().Sneeze ();
	}

	public void SneezeEnd()
	{
		GetComponentInChildren<Nose> ().SneezeEnd ();
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
