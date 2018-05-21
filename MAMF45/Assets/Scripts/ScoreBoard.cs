using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreBoard : MonoBehaviour {
	public static ScoreBoard Instance {
		get;
		private set;
	}
	public Text PointText;
	public Text ScoreBoardText;
	private int _points = 0;

    private int _amountBunnyHealthySaved;
    private int _amountBunnySaved;
    private int _amountBunnyDied;
    private int _amountMaterialRecycled;
    private int _amountBonusHole;
	private int _amountSneezePrevented;

    void Awake () {
        if (Instance != null) {
            Debug.LogError("More than one score board exists in the scene. Please make sure only to place one at a time!");
        } else {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void OnLevelWasLoaded(int level) {
		if (level == 0)
			ResetPoints();
    }

    public void BunnyHealthySaved() {
        GivePoints(Constants.Instance.ScoreBunnyHeartSaved, ScoreBoardText);
        _amountBunnyHealthySaved += Constants.Instance.ScoreBunnyHeartSaved;
    }

    public void BunnySaved() {
        GivePoints(Constants.Instance.ScoreBunnyNoHeartSaved, ScoreBoardText);
        _amountBunnySaved += Constants.Instance.ScoreBunnyNoHeartSaved;
    }

	public void BunnyDied(Text scoreText, bool killedByPlayer = false) {
		if (!killedByPlayer) {
			GivePoints (Constants.Instance.ScoreBunnyDied, scoreText);
			_amountBunnyDied += Constants.Instance.ScoreBunnyDied;
		} else {
			GivePoints (Constants.Instance.ScoreBunnyKilledByPlayer, ScoreBoardText);
			_amountBunnyDied += Constants.Instance.ScoreBunnyKilledByPlayer;
		}
    }

    public void MaterialRecycled(Text scoreText) {
        GivePoints(Constants.Instance.ScoreRecycle, scoreText);
        _amountMaterialRecycled += Constants.Instance.ScoreRecycle;
    }

    public void BonusHole(Text scoreText) {
        GivePoints(Constants.Instance.ScoreBonusHole, scoreText);
        _amountBonusHole += Constants.Instance.ScoreBonusHole;
    }

	public void SneezePrevented(Text scoreText) {
		GivePoints (Constants.Instance.ScoreSneezePrevented, scoreText);
		_amountSneezePrevented += Constants.Instance.ScoreSneezePrevented;
	}

    private void GivePoints(int points, Text scoreText) {
		_points += points;
		PointText.text = _points.ToString();
		if (points > 0)
			scoreText.text = "+" + points.ToString ();
		else
			scoreText.text = points.ToString ();
		scoreText.enabled = true;
		StartCoroutine (DisablePointPopup (scoreText));
	}

	private IEnumerator DisablePointPopup(Text scoreText) {
		yield return new WaitForSeconds (1.5f);
		scoreText.enabled = false;
	}

	public void ResetPoints() {
        _amountBunnyHealthySaved = 0;
        _amountBunnySaved = 0;
        _amountBunnyDied = 0;
        _amountMaterialRecycled = 0;
        _amountBonusHole = 0;
        _points = 0;
		PointText.text = _points.ToString();
}

    public int getAmountBunnyHealthySaved() {
        return _amountBunnyHealthySaved;
    }

    public int getAmountBunnySaved() {
        return _amountBunnySaved;
    }

    public int getAmountBunnyDied() {
        return _amountBunnyDied;
    }

    public int getAmountMaterialRecycled() {
        return _amountMaterialRecycled;
	}

	public int getAmountBonusHole() {
		return _amountBonusHole;
	}

	public int getAmountSneezePrevented() {
		return _amountSneezePrevented;
	}

	public int getPoints() {
        return _points;
    }
}
