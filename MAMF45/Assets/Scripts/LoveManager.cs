using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LoveManager : MonoBehaviour {

	public float Difficulty = 0;
	private float countdown;
	private Constants constants;
	
	void Start () {
		constants = Constants.Instance;
		countdown = constants.TimerLoveIntervals;
	}
	
	void Update () {
		if (Constants.Instance.HasGameBegun) {
			countdown -= Time.deltaTime;
			if (countdown <= 0) {
				countdown = Mathf.Max(constants.TimerLoveIntervals - Difficulty * 5, 5);
				CreateLove();
			}
		}
	}

	void CreateLove() {
		var bunniesWithoutPartners = new List<GameObject>(GameObject.FindGameObjectsWithTag("Bunny")).Where(b => !b.GetComponent<Lust>().HasPartner()).ToList();
		if (bunniesWithoutPartners.Count < 2)
			return;

		var clamydiaBunnies = bunniesWithoutPartners.Where(b => b.GetComponent<ClamydiaIllness>()).ToList();
		var sickBunnies = bunniesWithoutPartners.Where(b => !b.GetComponent<ClamydiaIllness>() && b.GetComponent<Illness>()).ToList();
		var healthyBunnies = bunniesWithoutPartners.Where(b => !b.GetComponent<Illness>()).ToList();

		GameObject bunny1 = null;
		GameObject bunny2 = null;

		float chance = Random.Range(0f, 1f);
		if (chance < 0.6f && clamydiaBunnies.Count > 0) { // Clamydia + other
			bunny1 = bunny2 = RandomBunny(clamydiaBunnies);
			chance = Random.Range(0f, 1f);
			if (chance < 0.6f && healthyBunnies.Count > 0) {
				bunny2 = RandomBunny(healthyBunnies);
			} else if (sickBunnies.Count > 0) {
				bunny2 = RandomBunny(sickBunnies);
			} else {
				while (bunny1 == bunny2) {
					bunny2 = RandomBunny(bunniesWithoutPartners);
				}
			}
		} else if (chance < 0.92f && sickBunnies.Count > 0) { // Sick bunny + other
			bunny1 = bunny2 = RandomBunny(sickBunnies);
			chance = Random.Range(0f, 1f);
			if (chance < 0.8f && healthyBunnies.Count > 0) {
				bunny2 = RandomBunny(healthyBunnies);
			} else {
				while (bunny1 == bunny2) {
					bunny2 = RandomBunny(bunniesWithoutPartners);
				}
			}
		} else { // Completely random
			bunny1 = bunny2 = RandomBunny(bunniesWithoutPartners);
			while (bunny1 == bunny2) {
				bunny2 = RandomBunny(bunniesWithoutPartners);
			}
		}

		bunny1.GetComponent<Lust>().SetPartner(bunny2, Mathf.Max(5, constants.TimerLoveReactionTime - Difficulty * 3));
		bunny2.GetComponent<Lust>().SetPartner(bunny1, Mathf.Max(5, constants.TimerLoveReactionTime - Difficulty * 3));
	}

	GameObject RandomBunny(List<GameObject> bunnies) {
		int n = Random.Range(0, bunnies.Count);
		return bunnies[n];
	}
}
