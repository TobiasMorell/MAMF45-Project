using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusHole : MonoBehaviour {

	private int score;

	private void Awake() {
		score = Constants.Instance.ScoreBonusHole;
	}

	private void OnTriggerEnter(Collider other) {
		GameObject.Find("ScoreBoard").GetComponent<ScoreBoard>().GivePoints(score);
	}
}
