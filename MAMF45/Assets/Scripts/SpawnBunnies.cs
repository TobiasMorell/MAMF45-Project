using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBunnies : MonoBehaviour {
	#pragma warning disable 0649
    [SerializeField]
    private GameObject bunnyPrefab;
    [SerializeField]
    private GameObject[] startingBunnies;
#pragma warning disable 0649

    private float cooldown = 5;
    private float swarm = 0;

    private float swarmCooldown = 60;
	private bool _isStarted = false;
	
	// Update is called once per frame
	void Update ()
    {
        if (Constants.Instance.HasGameBegun && !_isStarted)
        {
            _isStarted = true;
            foreach (GameObject bunny in startingBunnies)
                bunny.SetActive(true);
        }
		if (!_isStarted)
			return;

        cooldown -= Time.deltaTime;
        swarmCooldown -= Time.deltaTime;

        if (cooldown <= 0)
        {
			var chance = Random.Range (0f, 1f);
			if (chance > 0.66f)
				SpawnBunny (new ColdIllness ());
			else if (chance > 0.33f)
				SpawnBunny(new PneumoniaIllness());
			else 
				SpawnBunny(new ClamydiaIllness());
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

	private void SpawnBunny(Illness illness) {
		transform.position = new Vector3(Random.Range(-0.8f, 0.8f), transform.position.y, Random.Range(-0.8f, 0.8f));
		var b = Instantiate(bunnyPrefab, transform.position, transform.rotation);
		b.GetComponent<Health>().Infect(illness);
	}
}
