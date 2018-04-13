using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour {
	private enum ActionState
	{
		IDLE,
		MOVING
	}


	[SerializeField]
	private float movementSpeed = 0.2f;
	private float rotationSpeed = 5;
	[SerializeField]
	private float laziness = 2;

	private bool isRunning;
	private ActionState actionState;
	private float actionTime;
	private Quaternion direction;
	private Animator animator;

	private Vector3 target;

	new private Rigidbody rigidbody;

	void Start () {
		rigidbody = GetComponent<Rigidbody>();
		laziness = Random.Range(0f, 4f);
		animator = GetComponent<Animator> ();
		Restart ();
	}
	
	// FixedUpdate is called once per frame in sync with the physics engine
	void FixedUpdate () {
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
		switch (actionState)
		{
			case ActionState.IDLE:
				break;
			case ActionState.MOVING:
				if (transform.rotation != direction)
				rigidbody.MoveRotation(Quaternion.RotateTowards(transform.rotation, direction, rotationSpeed));
				else
					rigidbody.MovePosition(transform.position + transform.forward * movementSpeed * Time.fixedDeltaTime);
				break;
		}
	}

	private void UpdateActionState()
	{
		if (actionState == ActionState.IDLE)
		{
			actionState = ActionState.MOVING;
			actionTime = Random.Range(0.5f, 3.5f);

			target = new Vector3(Random.Range(-1f, 1f), transform.position.y, Random.Range(-1f, 1f));
			var delta = Vector3.SignedAngle(transform.forward, target - transform.position, Vector3.up);
			direction = transform.rotation * Quaternion.Euler(0, delta, 0);

			animator.SetTrigger ("Move");
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


	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(target, 0.01f);
	}
}
