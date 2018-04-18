using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBunnies : MonoBehaviour {

    [SerializeField]
    private GameObject bunnyPrefab;
    private float cooldown = 5;
    private float swarm = 0;

    private float swarmCooldown = 60;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        cooldown -= Time.deltaTime;
        swarmCooldown -= Time.deltaTime;

        if (cooldown <= 0)
        {
            transform.position = new Vector3(Random.Range(-0.8f, 0.8f), transform.position.y, Random.Range(-0.8f, 0.8f));
            var b = Instantiate(bunnyPrefab, transform.position, transform.rotation);
            b.GetComponent<Health>().Infect();
            if (swarm > 0)
            {
                swarm -= 1;
                cooldown += 1;
            }
            else
                cooldown += 30;

        }

        if (swarmCooldown <= 0)
        {
            swarm = 5;
            swarmCooldown += 150;
        }
    }
}
