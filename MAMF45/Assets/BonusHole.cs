using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusHole : MonoBehaviour {

	private void OnTriggerEnter(Collider other) {
		GameObject.Find("ScoreBoard").GetComponent<ScoreBoard>().GivePoints(50);
	}
}
