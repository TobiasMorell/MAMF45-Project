using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoveManager : MonoBehaviour {

	public float MaxCountdown = 30;
	public float MaxReactionTime = 15;
	public float Difficulty = 0;
	private float countdown;

	// Use this for initialization
	void Start () {
		countdown = MaxCountdown;
	}
	
	// Update is called once per frame
	void Update () {
		countdown -= Time.deltaTime;
		if (countdown <= 0) {
			countdown = Mathf.Max(MaxCountdown - Difficulty * 5, 5);
			var bunnies = GameObject.FindGameObjectsWithTag ("Bunny");
			var b1 = -1;
			var b2 = -1;
			for (var i = 0; i < 100; ++i) {
				int n = Random.Range (0, bunnies.Length);
				if (!bunnies [n].GetComponent<Lust> ().HasPartner()) {
					if (b1 == -1)
						b1 = n;
					else if (b1 != n) {
						b2 = n;
						break;
					}						
				}
			}
			if (b1 != -1 && b2 != -1) {
				bunnies[b1].GetComponent<Lust>().SetPartner(bunnies[b2], Mathf.Max(5,MaxReactionTime-Difficulty*3));
				bunnies[b2].GetComponent<Lust>().SetPartner(bunnies[b1], Mathf.Max(5,MaxReactionTime-Difficulty*3));
			}
		}
	}
}
