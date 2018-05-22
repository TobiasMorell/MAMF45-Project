using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

        cooldown -= Time.deltaTime * (Constants.Instance.MaxBunnyCount - GameObject.FindGameObjectsWithTag(Tags.BUNNY).Length);
        swarmCooldown -= Time.deltaTime;

        if (cooldown <= 0)
        {
			Spawn ();
        }

        if (swarmCooldown <= 0)
        {
            swarm = 5;
            swarmCooldown += Constants.Instance.SwarmRate;
        }
    }

	private IllnessTypes GetRandomDisease(Dictionary<IllnessTypes, float> stackedChances, float random) {
		foreach (var item in stackedChances) {
			if (random > item.Value)
				continue;
			return item.Key;
		}
		return IllnessTypes.Death;
	}

	private Dictionary<IllnessTypes, float> BuildStackedChanceStructure(Dictionary<IllnessTypes, float> chances) {
		var nd = new Dictionary<IllnessTypes, float> ();
		float sum = 0;
		var dictLog = "";
		foreach (var c in chances) {
			nd [c.Key] = c.Value + sum;
			sum += c.Value;

			dictLog += c.Key + ": " + c.Value + "   ";
		}
		Debug.Log (dictLog);
		return nd;
	}

	private Dictionary<IllnessTypes, float> CalculateDiseaseChances() {
		var ic = CountBunnies (GameObject.FindGameObjectsWithTag (Tags.BUNNY).Select(b => b.GetComponent<Health>()));
		var chances = new Dictionary<IllnessTypes, float> {
			{IllnessTypes.Cold, Constants.Instance.ChanceCold},
			{IllnessTypes.Pneumenia, Constants.Instance.ChancePneunemia},
			{IllnessTypes.Clamydia, Constants.Instance.ChanceChlamydia}
		};

		if (ic.Count > 0) {
			var totalBunnies = ic.Count; // TODO totalBunnies == amount of different diseases! This is wrong.
			var mostCommon = IllnessTypes.Death;
			var count = -1;

			foreach (var c in ic) {
				Debug.Log (c.Key + ": " + c.Value);
				if (c.Value > count) {
					mostCommon = c.Key;
					count = c.Value;
				}
			}
			var share = count / (float)totalBunnies;
			var scaling = share * Constants.Instance.RareDiseaseScalingFactor;
			Debug.Log ("share: " + share + ", scaling: " + scaling);

			var scaledChances = new Dictionary<IllnessTypes, float> ();
			foreach (var chance in chances) {
				if (chance.Key == mostCommon)
					scaledChances [chance.Key] = chance.Value - scaling;
				else
					scaledChances [chance.Key] = chance.Value + scaling;
			}

			return scaledChances;
		}
		return chances;
	}

	private Dictionary<IllnessTypes, int> CountBunnies(IEnumerable<Health> bunnies) {
		var illnessCounts = new Dictionary<IllnessTypes, int>();

		foreach (var h in bunnies) {
			var ils = h.GetIllnesses ();
			foreach (var i in ils) {
				var it = i.GetIllnessType ();
				if (!illnessCounts.ContainsKey (it))
					illnessCounts [it] = 0;
				else
					illnessCounts [it]++;
			}
		}

		return illnessCounts;
	}

	private void Spawn() {
		var chances = CalculateDiseaseChances ();
		var it = GetRandomDisease (BuildStackedChanceStructure (chances), Random.Range(0f, 1f));
		Debug.Log ("Spawning a bunny with: " + it);

		if (it == IllnessTypes.Cold)
			SpawnBunny (new ColdIllness ());
		else if (it == IllnessTypes.Pneumenia)
			SpawnBunny (new PneumoniaIllness ());
		else if (it == IllnessTypes.Clamydia)
			SpawnBunny (new ClamydiaIllness ());
		else {
			SpawnBunny (new ColdIllness (), new PneumoniaIllness ());
		}
		if (swarm > 0) {
			swarm -= 1;
			cooldown += 1;
		} else
			cooldown += Constants.Instance.SpawnRate;
	}

	private void SpawnBunny(params Illness[] illnesses) {
		transform.position = new Vector3(Random.Range(-0.8f, 0.8f), transform.position.y, Random.Range(-0.8f, 0.8f));
		var b = Instantiate(bunnyPrefab, transform.position, transform.rotation);
		foreach (var illness in illnesses) {
			b.GetComponent<Health>().Infect(illness);
		}
	}
}
