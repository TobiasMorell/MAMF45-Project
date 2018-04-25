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

	private HashSet<Illness> illnesses;
	private bool isIll;

	private bool isProtected;
	private GameObject contraceptive = null;

    private Animator animator;
    private BasicMovement movement;

    private void Awake()
    {
		illnesses = new HashSet<Illness> ();
        animator = GetComponent<Animator>();
        movement = GetComponent<BasicMovement>();
    }

    void Start ()
	{
		if (StartInfected) {
			//An instance is manually spawned here - all other illnesses are clones of this
			Infect (new ColdIllness());
		}
	}

	void Update ()
	{
		
	}


	public void Infect (params Illness[] illnesses)
	{
		foreach (var illness in illnesses) {
			var details = Illnesses.GetDetails (illness.GetIllnessType());

			if (this.illnesses.Contains (illness)) {
				print ("Already has illness: " + illness);
			} else if (contraceptive) {
				print ("Prevented by contraceptive: " + illness);
			} else {
				this.illnesses.Add(illness.Infect(gameObject));
				print ("New infection!");

				UpdateIllnessAppearance ();
			}

			if (illness.GetType ().IsSubclassOf (typeof(SneezeIllness))) {
				GetComponentInChildren<Nose> ().ResetSneezeTimer ();
			}
		}
	}

	public void Cure (Illness illness) {
		if (illnesses.Remove (illness)) {
			UpdateIllnessAppearance ();
			animator.SetTrigger ("Cured");
			Destroy (illness);
		}
	}

	public void Cure ()
	{
		
		foreach (var illness in illnesses)
		{
			if (illness.Cure ()) {
				Destroy (illness);

				UpdateIllnessAppearance ();
				animator.SetTrigger ("Cured");
			} else {
				print ("Uncurable illness!");
			}
        }
		print("Removed: " + illnesses.RemoveWhere(i=>!i));
	}

	private void UpdateIllnessAppearance() {
		var sicknessProperty = GetComponentInChildren<SicknessMaterialBlockProperty> ();
		var billboard = GetComponentInChildren<Billboard> ();
		billboard.ClearDiseases ();

		bool isIll = illnesses.Count > 0;
		if (isIll && !this.isIll) {
			sicknessProperty.ToggleSickness (true);
			sicknessCloudInstance = Instantiate (SicknessCloudEffect, transform);
			Instantiate (SneezeHitParticleEffect, transform.position, transform.rotation);
			animator.SetFloat ("SicknessBlend", 1.0f);
		}

		if (!isIll && this.isIll) {
			sicknessProperty.ToggleSickness (false); 
			Destroy (sicknessCloudInstance);
			animator.SetFloat ("SicknessBlend", 0f);
		}

		if (isIll) {
			Color color = Color.clear;
			foreach (var illness in illnesses) {
				var details = Illnesses.GetDetails (illness.GetIllnessType());
				color += details.color;

				billboard.AddDisease (details);
			}
			sicknessProperty.SicknessColor = color/illnesses.Count;
		}

		this.isIll = isIll;
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
