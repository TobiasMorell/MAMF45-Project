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

	// Use this for initialization
	void Awake () {
		if (Instance != null)
			Debug.LogError ("More than one score board exists in the scene. Please make sure only to place one at a time!");
		Instance = this;
	}

	public void GivePoints(int points) {
		_points += points;
		PointText.text = _points.ToString();
	}

	public void ResetPoints() {
		_points = 0;
		PointText.text = _points.ToString();
	}
}
