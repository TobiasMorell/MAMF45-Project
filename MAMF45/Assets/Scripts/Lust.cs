using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lust : MonoBehaviour {
	private static float LOVE_RANGE = 0.15f;
	private static float MAX_CHASE_TIME = 15;

	#pragma warning disable 0649
    [SerializeField]
    private GameObject heartEffect;
	[SerializeField]
	private GameObject loveEffect;
	#pragma warning restore 0649

	private ParticleSystem.EmissionModule heartEmissionModule;
	public float drive;
    private bool isLoving;
    public float rangeScale;

	private float overDrive;

	private GameObject target = null;

	private BasicMovement movement;
	private Animator animator;

	// Use this for initialization
	void Awake () {
		drive = Random.Range(30.0f, 60.0f);
		var hearts = Instantiate(heartEffect, transform);
		heartEmissionModule = hearts.GetComponent<ParticleSystem>().emission;
		movement = GetComponent<BasicMovement>();
		animator = GetComponent<Animator>();
        isLoving = false;
		overDrive = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isLoving && target)
        {
			if (drive == 0)
            {
				overDrive += Time.deltaTime;
				if (Vector3.Distance (target.transform.position, transform.position) < LOVE_RANGE * rangeScale) {
					target.GetComponent<Lust>().Love();
					Love();
				} else if (overDrive > MAX_CHASE_TIME) {
					target = null;
					isLoving = false;
					movement.ResetTarget();
					drive = 10000;
				}
            }
			else
            {
				drive -= Time.deltaTime;
                if (drive <= 0)
                {
                    drive = 0;
                    rangeScale = (transform.localScale.z * 1.1f + target.transform.localScale.z * 1.1f) /2;
                    movement.SetTarget(target);
                }
            }
        }
        if (target)
            heartEmissionModule.rateOverTime = 5 / (drive / 5 + 1);
        else
            heartEmissionModule.rateOverTime = 0;

    }

	public void Love()
	{
        isLoving = true;
        movement.ResetTarget();
        movement.Stop();
		drive = 0;
		animator.SetTrigger("Love");
        target = null;
    }

	void LoveDone()
    {
        target = null;
        isLoving = false;
        movement.Restart();
        drive = Random.Range(45.0f, 60.0f);
        Instantiate(loveEffect, transform.position, transform.rotation);
	}

	public void StopLove() {
		target = null;
		isLoving = false;
		drive = 10000000;
		heartEmissionModule.rateOverTime = 0;
	}


	public bool HasPartner() {
		return target != null;
	}

	public void SetPartner(GameObject partner, float countdown) {
		target = partner;
		drive = countdown;
	}

    private void OnDrawGizmos()
    {
        if (target != null) {
            Gizmos.DrawCube(target.transform.position, Vector3.one/10);
            Gizmos.DrawLine(transform.position, Vector3.MoveTowards(transform.position, target.transform.position, LOVE_RANGE * rangeScale));
        }

    }
}
