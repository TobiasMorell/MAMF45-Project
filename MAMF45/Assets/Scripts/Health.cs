using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    private int MAXIMUM_SNEEZE_INTERVAL = 5;
    private float SNEEZE_RADIUS = 0.2f;

    public bool StartInfected = false;

    private bool isSick;
    private float sneezeTimer;

	void Start ()
    {
        sneezeTimer = Random.Range(0f, MAXIMUM_SNEEZE_INTERVAL);
        isSick = StartInfected;
	}
	
	void Update ()
    {
        sneezeTimer -= Time.deltaTime;
        if (sneezeTimer < 0)
        {
            var colliders = Physics.OverlapSphere(transform.position + transform.forward * SNEEZE_RADIUS, SNEEZE_RADIUS);
            foreach (var collider in colliders)
            {
                var health = collider.GetComponent<Health>();
                if (collider.gameObject != gameObject && health != null)
                {
                    health.Infect();
                }
            }
            sneezeTimer = Random.Range(0f, MAXIMUM_SNEEZE_INTERVAL);
        }
	}


    public void Infect()
    {
        if (!isSick)
        {
            print("New rabbit infected!");
        }
        isSick = true;
    }

	public void Cure() 
	{
		isSick = false;
	}
		

    public bool IsSick()
    {
        return isSick;
    }

    public bool IsHealthy()
    {
        return !isSick;
    }

    void OnDrawGizmos()
    {
        if (isSick)
        {
            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawWireSphere(transform.position + transform.forward * SNEEZE_RADIUS, SNEEZE_RADIUS);
        }
    }
}
