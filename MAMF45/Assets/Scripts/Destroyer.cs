using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Destroyer : MonoBehaviour {

	private HashSet<Destructible> recycled;
	public Text ScoreText;

	void Start() {
		recycled = new HashSet<Destructible> ();
		if (ScoreText != null) {
			ScoreText.text = "+" + Constants.Instance.ScoreRecycle;
			ScoreText.enabled = false;
		}
	}

	void OnTriggerEnter(Collider collider) {
		var destructible = collider.GetComponentInChildren<Destructible> ();
		if (!destructible)
			destructible = collider.GetComponentInParent<Destructible> ();
		if (destructible && !destructible.IsHeld()) {
			Destroy (destructible.gameObject, 5.0f);

			if (!recycled.Contains (destructible)) {
				ScoreBoard.Instance.MaterialRecycled (ScoreText);
				recycled.Add(destructible);
			}
		}
	}
}
