using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
	public bool StartInfected = false;

	public GameObject SneezeParticleEffect;
	public GameObject SneezeHitParticleEffect;
	public GameObject SicknessCloudEffect;

	private GameObject sicknessCloudInstance = null;

	private HashSet<Illness> illnesses;
	private bool isIll;

	private GameObject contraceptive = null;

	private Animator animator;
	private BasicMovement movement;

	private float _healthyTimer;
	private Billboard _billboard;

	private void Awake()
	{
		illnesses = new HashSet<Illness> ();
		animator = GetComponent<Animator>();
		movement = GetComponent<BasicMovement>();
		_billboard = GetComponentInChildren<Billboard> ();
	}

	public HashSet<Illness> GetIllnesses() {
		return new HashSet<Illness> (illnesses);
	}

	void Start () {
		if (StartInfected) {
			//An instance is manually spawned here - all other illnesses are clones of this
			Infect (new ColdIllness());
			//Infect (new PneumoniaIllness());
		}
	}

	void Update ()
	{
		if (isIll)
			_healthyTimer = 0;
		else {
			_healthyTimer += Time.deltaTime;
			if (_healthyTimer >= Constants.Instance.TimerTriggerHealthyBunny) {
				_billboard.DisplayHealthy ();
			}
		}
	}

	public bool GivesPoints {
		get {
			return _healthyTimer >= Constants.Instance.TimerTriggerHealthyBunny;
		}
	}


	public void Infect (params Illness[] illnesses)
	{
		if (GetComponent<Despawner> ())
			return;
		
		foreach (var illness in illnesses) {
			if (this.illnesses.Contains (illness)) {
				//print ("Already has illness: " + illness);
			} else if (contraceptive) {
				//print ("Prevented by contraceptive: " + illness);
			} else {
				var i = illness.Infect (gameObject);
				this.illnesses.Add(i);
				_billboard.AddIllness(i, i.GetUITimerMax());
				//print ("New infection!");

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
			_billboard.RemoveIllness (illness);

			animator.SetTrigger ("Cured");
			Destroy (illness);
		}
	}

	public void Cure ()
	{
		var removed = new List<Illness> ();
		foreach (var illness in illnesses)
		{
			if (illness.Cure ()) {
				_billboard.RemoveIllness (illness);
				Destroy (illness);
				removed.Add (illness);
			} else {
				print ("Uncurable illness!");
			}
		}
		if (illnesses.RemoveWhere (i => removed.Contains (i)) > 0) {
			UpdateIllnessAppearance ();
			animator.SetTrigger ("Cured");
		}
	}

	private void UpdateIllnessAppearance() {
		var sicknessProperty = GetComponentInChildren<SicknessMaterialBlockProperty> ();
		var illnessDetails = illnesses.Select(i => Illnesses.GetDetails(i.GetIllnessType())).ToArray();

		bool isIll = illnesses.Count > 0;
		if (isIll && !this.isIll) {
			sicknessCloudInstance = Instantiate (SicknessCloudEffect, transform);
			Instantiate (SneezeHitParticleEffect, transform.position, transform.rotation);
			animator.SetFloat ("SicknessBlend", 1.0f);
		}

		if (!isIll && this.isIll) { 
			Destroy (sicknessCloudInstance);
			animator.SetFloat ("SicknessBlend", 0f);
		}

		if (isIll) {
			Color color = Color.clear;
			foreach (var illness in illnessDetails) {
				color += illness.color;
			}
			sicknessProperty.InterpolateTo (color / illnesses.Count);
		} else {
			sicknessProperty.InterpolateToDefault ();
		}
		if (illnesses.RemoveWhere (i => !i) > 0) {
			UpdateIllnessAppearance ();
			animator.SetTrigger ("Cured");
		}

		this.isIll = isIll;
	}

	public void Die()
	{
		GetComponentInChildren<SicknessMaterialBlockProperty> ().SetDefaultColorFraction (0.9f);

		animator.SetTrigger ("Die");
		foreach (var illness in illnesses) {
			_billboard.RemoveIllness (illness);
			Destroy (illness);
		}
		illnesses.Clear ();

		GetComponent<Lust> ().StopLove ();
		GetComponent<Lust> ().enabled = false;

		GetComponent<AudioSource>().Stop();

		Infect (new DeathIllness());
		var d = gameObject.AddComponent<Despawner> ();
		d.ToggleDeath ();

		ScoreBoard.Instance.BunnyDied (GetComponentInChildren<Text>());
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
			contraceptive = other.transform.parent.gameObject;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag(Tags.CONTRACEPTIVE))
		{
			contraceptive = null;
		}
	}
}
