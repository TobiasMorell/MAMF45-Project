using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lust : MonoBehaviour {
	private static float LOVE_RANGE = 0.1f;

    [SerializeField]
    private GameObject heartEffect;
	[SerializeField]
	private GameObject loveEffect;
	private ParticleSystem.EmissionModule heartEmissionModule;
	public float drive;
    private bool isLoving;

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
	}
	
	// Update is called once per frame
	void Update () {
        if (!isLoving)
        {
            if (target)
            {
                if (Vector3.Distance(target.transform.position, transform.position) < LOVE_RANGE)
                {
                    movement.ResetTarget();
                    target.GetComponent<Lust>().Love();
                    target = null;
                    Love();
                }
            }
            else
            {
                if (GameObject.FindGameObjectsWithTag(gameObject.tag).Length > 1)
                    drive -= Time.deltaTime;
                else if (drive < 30)
                    drive += 30;
                if (drive <= 0)
                {
                    drive = 0;
                    var objects = GameObject.FindGameObjectsWithTag(gameObject.tag);
                    do
                    {
                        target = objects[Random.Range(0, objects.Length)];
                    } while (target == gameObject);

                    movement.SetTarget(target);
                }
            }
        }
        if (drive < 30)
            heartEmissionModule.rateOverTime = 5 / (drive / 5 + 1);
        else
            heartEmissionModule.rateOverTime = 0;

    }

	public void Love()
	{
        isLoving = true;
        movement.Stop();
		drive = 0;
		animator.SetTrigger("Love");
	}

	void LoveDone()
    {
        isLoving = false;
        movement.Restart();
        drive = Random.Range(45.0f, 60.0f);
        Instantiate(loveEffect, transform.position, transform.rotation);
	}

    private void OnDrawGizmos()
    {
        if (target != null) {
            Gizmos.DrawCube(target.transform.position, Vector3.one/20);
            Gizmos.DrawLine(transform.position, transform.position + Vector3.MoveTowards(transform.position, target.transform.position, 0.1f));
        }

    }
}
