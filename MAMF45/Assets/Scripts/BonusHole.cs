using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusHole : MonoBehaviour {
	public Text scoreText;

	private void OnTriggerEnter(Collider other) {
		GameObject.Find("ScoreBoard").GetComponent<ScoreBoard>().GivePoints(Constants.Instance.ScoreBonusHole, scoreText);
	}
}
