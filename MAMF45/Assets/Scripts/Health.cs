﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

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

	public float HealthyTimeToPoint = 60f;
	private float _healthyTimer;
	private Billboard _billboard;

    private void Awake()
    {
		illnesses = new HashSet<Illness> ();
        animator = GetComponent<Animator>();
        movement = GetComponent<BasicMovement>();
		_billboard = GetComponentInChildren<Billboard> ();
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
		if (isIll)
			_healthyTimer = 0;
		else {
			_healthyTimer += Time.deltaTime;
			if (_healthyTimer >= HealthyTimeToPoint) {
				_billboard.DisplayHealthy ();
			}
		}
	}

	public bool GivesPoints {
		get {
			return _healthyTimer >= HealthyTimeToPoint;
		}
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
			} else {
				print ("Uncurable illness!");
			}
        }
		if (illnesses.RemoveWhere (i => !i) > 0) {
			UpdateIllnessAppearance ();
			animator.SetTrigger ("Cured");
			Debug.Log ("I HAS CURED!");
		}
	}

	private void UpdateIllnessAppearance() {
		var sicknessProperty = GetComponentInChildren<SicknessMaterialBlockProperty> ();
		_billboard.ClearDiseases ();
		var illnessDetails = illnesses.Select(i => Illnesses.GetDetails(i.GetIllnessType())).ToArray();

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
			foreach (var illness in illnessDetails) {
				color += illness.color;
			}
			sicknessProperty.SicknessColor = color/illnesses.Count;

			_billboard.DisplayDiseases (illnessDetails);
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
		if (other.CompareTag(Tags.CONTRACEPTIVE))
		{
			isProtected = true;
			contraceptive = other.transform.parent.gameObject;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag(Tags.CONTRACEPTIVE))
		{
			isProtected = false;
			contraceptive = null;
		}
	}
}
