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
	private float movementSpeed = 2;
	[SerializeField]
	private float laziness = 2;

	private ActionState actionState;
	private float actionTime;
	private Quaternion direction;

	new private Rigidbody rigidbody;

	void Start () {
		actionTime = Random.Range(laziness / 2f, laziness);
		actionState = ActionState.IDLE;
		rigidbody = GetComponent<Rigidbody>();
	}
	
	// FixedUpdate is called once per frame in sync with the physics engine
	void FixedUpdate () {
		Act();
		actionTime -= Time.fixedDeltaTime;
		if (actionTime <= 0)
		{
			UpdateActionState();
		}
	}

	private void Act()
	{
		switch (actionState)
		{
			case ActionState.IDLE:
				break;
			case ActionState.MOVING:
				if (Quaternion.Dot(transform.rotation, direction) < 0.9f)
					rigidbody.MoveRotation(Quaternion.RotateTowards(transform.rotation, direction, movementSpeed*8));
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
			actionTime = Random.Range(0.2f, 2f);

			rigidbody.drag = 0;
		}
		else if (actionState == ActionState.MOVING)
		{
			actionState = ActionState.IDLE;
			actionTime = Random.Range(laziness / 2f, laziness);
			direction = transform.rotation * Quaternion.Euler(0, Random.Range(-180,180), 0);

			rigidbody.drag = 0;
		}
	}
}
