﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

public class BasicMovement : MonoBehaviour {
	private enum ActionState
	{
		IDLE,
		MOVING,
		MOVING_DONT_STOP,
		CARRIED
	}


	[SerializeField]
	private float movementSpeed = 0.2f;
	private float rotationSpeed = 1;
	[SerializeField]
	private float laziness = 2;

	private bool isRunning;
	private bool doneTurning;
	private ActionState actionState;
	private float actionTime;
	private Quaternion direction;
	private Animator animator;

	private Vector3 target;
	private GameObject targetObject;

	new private Rigidbody rigidbody;

	private bool _waitingForGroundCollision = false;
	private bool _outsideFence = false;
    private bool _hasGivenPoints = false;

    public bool IsSaved {
		get;
		private set;
	} 

	void Start () {
		rigidbody = GetComponent<Rigidbody>();
		laziness = Random.Range(0f, 4f);
		animator = GetComponent<Animator> ();
		Restart ();
	}
	
	// FixedUpdate is called once per frame in sync with the physics engine
	void FixedUpdate () {
		if (targetObject)
		{
			target = targetObject.transform.position;
            target.y = transform.position.y;
            var delta = Vector3.SignedAngle(transform.forward, target - transform.position, Vector3.up);
            direction = transform.rotation * Quaternion.Euler(0, delta, 0);
		}
		if (isRunning) {
			Act ();
			actionTime -= Time.fixedDeltaTime;
			if (actionTime <= 0) {
				UpdateActionState ();
			}
		}
	}

	private void Act()
	{
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("NoMove"))
            return;
		switch (actionState)
		{
			case ActionState.IDLE:
				break;
			case ActionState.MOVING:
			case ActionState.MOVING_DONT_STOP:
				if (transform.rotation != direction) {
					rigidbody.MoveRotation (Quaternion.RotateTowards (transform.rotation, direction, rotationSpeed));
				} else {
					if (!doneTurning)
					{
						animator.SetTrigger("Move");
						doneTurning = true;
					}
					rigidbody.MovePosition(transform.position + transform.forward * movementSpeed * Time.fixedDeltaTime);
				}
				break;
			case ActionState.CARRIED:
				break;
		}
	}

	private void UpdateActionState()
	{
		if (actionState == ActionState.IDLE)
		{
			if (IsSaved) {
				actionState = ActionState.MOVING_DONT_STOP;
				actionTime = 1000000;
			} else {
				actionState = ActionState.MOVING;
				actionTime = Random.Range (0.5f, 3.5f);
			}

			target = new Vector3(Random.Range(-1f, 1f), transform.position.y, Random.Range(-1f, 1f));
			var delta = Vector3.SignedAngle(transform.forward, target - transform.position, Vector3.up);
			direction = transform.rotation * Quaternion.Euler(0, delta, 0);

			animator.SetTrigger ("Turn");
			doneTurning = false;
		}
		else if (actionState == ActionState.MOVING)
		{
			actionState = ActionState.IDLE;
			actionTime = Random.Range(laziness / 2f, laziness);

			animator.SetTrigger ("Idle");
		}
	}


	public void Stop() {
		isRunning = false;
	}

	public void Restart() {
		actionTime = Random.Range(laziness / 2f, laziness);
		actionState = ActionState.IDLE;
		isRunning = true;
	}

	public void SetTarget(GameObject target)
	{
		this.targetObject = target;
	}

	public void ResetTarget()
	{
		targetObject = null;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(target, 0.01f);
	}

	public void BeginBunnyPickup() {
		animator.SetTrigger ("PickedUp");
		actionState = ActionState.CARRIED;
	}
	public void EndBunnyPickup() {
		_waitingForGroundCollision = true;
	}

	void OnCollisionEnter(Collision collision) {
		//If the bunny gives points, it means that it's healthy and should move towards the horizon
		if (_outsideFence && !_hasGivenPoints) {
			GetComponent<Lust> ().StopLove ();

			var scoreText = GetComponentInChildren<Text> ();
			var health = GetComponent<Health> ();
			var billboard = GetComponentInChildren<Billboard> ();
			billboard.RemoveIcons ();

			if (health.GivesPoints) {
				ScoreBoard.Instance.BunnyHealthySaved ();
				ToggleSavedBehaviour ();
			} else if (health.IsSick ()) {
				health.Die (true);
			} else {
				ScoreBoard.Instance.BunnySaved ();
				ToggleSavedBehaviour ();
			}
            
            Destroy(GetComponent<Throwable>());
            Destroy(GetComponent<VelocityEstimator>());
            Destroy(GetComponent<Interactable>());

            _hasGivenPoints = true;

            gameObject.tag = Tags.DEFAULT;
        }

		if (_waitingForGroundCollision) {
			animator.SetTrigger ("Dropped");
			_waitingForGroundCollision = false;
			actionState = ActionState.IDLE;
		}
	}

	private void ToggleSavedBehaviour() {
		AssignClosestFinishPoint ();
		StartCoroutine (DespawnAfterDelay ());
		GetComponent<Health> ().enabled = false;
		IsSaved = true;
    }
		
    void OnTriggerExit(Collider other) {
        if (!other.CompareTag(Tags.FENCE))
            return;

        _outsideFence = true;
    }

    void OnTriggerEnter(Collider other) {
        if (!other.CompareTag(Tags.FENCE))
            return;

        if (!_hasGivenPoints)
            _outsideFence = false;
    }

    private void AssignClosestFinishPoint() {
		var targets = GameObject.FindGameObjectsWithTag ("Finish");
		var shortestDist = float.MaxValue;
		GameObject closestTarget = null;

		foreach (var tar in targets) {
			var dist = Vector3.Distance (transform.position, tar.transform.position);

			if (dist < shortestDist) {
				shortestDist = dist;
				closestTarget = tar;
			}
		}

		SetTarget(closestTarget);
	}

	IEnumerator DestroyAfterDelay(GameObject go, float delaySeconds) {
		yield return new WaitForSeconds (delaySeconds);
		Destroy (go);
	}

	IEnumerator DespawnAfterDelay() {
		yield return new WaitForSeconds(Constants.Instance.BunnyDespawnDelay);
		gameObject.AddComponent<Despawner> ();
		animator.SetTrigger ("Idle");
	}
}
