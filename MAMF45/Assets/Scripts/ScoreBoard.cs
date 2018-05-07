using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour {
	public static ScoreBoard Instance {
		get;
		private set;
	}
	public Text PointText;
	private int _points = 0;
	public Text PointPopup;
	private Coroutine _disablePopupCoroutine;

	// Use this for initialization
	void Awake () {
		if (Instance != null)
			Debug.LogError ("More than one score board exists in the scene. Please make sure only to place one at a time!");
		Instance = this;
	}

	public void GivePoints(int points) {
		if (_disablePopupCoroutine != null)
			StopCoroutine (_disablePopupCoroutine);
		_points += points;
		PointText.text = _points.ToString();
		if (points > 0)
			PointPopup.text = "+" + points.ToString ();
		else
			PointPopup.text = points.ToString ();
		PointPopup.enabled = true;
		_disablePopupCoroutine = StartCoroutine (DisablePointPopup ());
	}

	private IEnumerator DisablePointPopup() {
		yield return new WaitForSeconds (1.5f);
		PointPopup.enabled = false;
	}

	public void ResetPoints() {
		_points = 0;
		PointText.text = _points.ToString();
	}
}
