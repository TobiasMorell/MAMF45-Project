using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultTableReader : MonoBehaviour {
    public Text TextBunnyHealthySaved;
    public Text TextBunnySaved;
	public Text TextBunnyDied;
    public Text TextSneezePrevented;
	public Text TextMaterialRecycled;
    public Text TextBonusHole;
    public Text TextTotal;

    void Start () {
        TextBunnyHealthySaved.text = "" + ScoreBoard.Instance.getAmountBunnyHealthySaved();
        TextBunnySaved.text = "" + ScoreBoard.Instance.getAmountBunnySaved();
        TextBunnyDied.text = "" + ScoreBoard.Instance.getAmountBunnyDied();
		TextSneezePrevented.text = "" + ScoreBoard.Instance.getAmountSneezePrevented();
		TextMaterialRecycled.text = "" + ScoreBoard.Instance.getAmountMaterialRecycled();
		TextBonusHole.text = "" + ScoreBoard.Instance.getAmountBonusHole();
        TextTotal.text = "" + ScoreBoard.Instance.getPoints();
    }
}
