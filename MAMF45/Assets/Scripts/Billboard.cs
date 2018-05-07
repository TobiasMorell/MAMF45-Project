using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using System;
using System.Collections.Generic;

public class Billboard : MonoBehaviour {
	private class IllnessCooldown {
		public IllnessAsset Illness;
		public float Cooldown;
		public float Timer = 0f;

		public IllnessCooldown (IllnessAsset illness, float cooldown)
		{
			Illness = illness;
			Cooldown = cooldown;
		}
	}

	public Sprite HealthyIcon;
	public Image[] icons;
	public Slider[] sliders;

	private List<IllnessCooldown> _illnesses;
	private bool _isHealthy;

	void Awake() {
		_illnesses = new List<IllnessCooldown> ();

		Vector3 scaleTmp = transform.localScale;
		scaleTmp.x /= transform.parent.localScale.x;
		scaleTmp.y /= transform.parent.localScale.y;
		scaleTmp.z /= transform.parent.localScale.z;
		transform.localScale = scaleTmp;
	}

	void Update()
	{
		DisplayIcons ();
	}

	private void DisplayIcons() {
		ClearIcons ();
		RedrawIcons ();
	}

	public void RemoveIcons() {
		ClearIcons ();
		_isHealthy = false;
	}

	private void ClearIcons() {
		Array.ForEach (icons, i => Clear (i));
		Array.ForEach (sliders, s => s.value = 0);
	}

	private void Clear(Image image) {
		image.enabled = false;
	}

	public void DisplayHealthy() {
		_isHealthy = true;
		SetupForThreeImages ();
	}

	private void RedrawIcons() {
		if (_isHealthy) {
			Display (icons [0], HealthyIcon);
		} else {
			var count = 0;
			for (var i = 0; i < _illnesses.Count; i++) {
				_illnesses [i].Timer += Time.deltaTime;

				count++;
				Display (icons [i], _illnesses [i].Illness.Icon);
				if (_illnesses [i].Cooldown != -1) {
					RedrawSlider (sliders [i], _illnesses [i]);
				} else {
					sliders [i].enabled = false;
				}
			}

			if (count == 2)
				SetupForTwoImages ();
			else
				SetupForThreeImages ();
		}
	}

	private void RedrawSlider(Slider slider, IllnessCooldown ic) {
		slider.maxValue = ic.Cooldown;
		slider.value = ic.Timer;
		slider.enabled = true;
	}

	public void AddIllness(Illness illness, float cooldown) {
		var ia = Illnesses.GetDetails (illness.GetIllnessType());
		_illnesses.Add (new IllnessCooldown(ia, cooldown));
		_isHealthy = false;
	}
	public void AddIllness(Illness illness) {
		AddIllness (illness, -1);
	}

	public void RemoveIllness(Illness illness) {
		var ia = Illnesses.GetDetails (illness.GetIllnessType());
		_illnesses.RemoveAll (ic => ic.Illness == ia);
	}

	private void Display(Image image, Sprite icon) {
		if (icon == null)
			return;

		image.sprite = icon;
		image.enabled = true;
	}

	private void SetupForTwoImages() {
		icons [0].rectTransform.anchoredPosition = new Vector2 (-64, 0);
		icons [1].rectTransform.anchoredPosition = new Vector2 (64, 0);
	}

	private void SetupForThreeImages() {
		icons [0].rectTransform.anchoredPosition = new Vector2 (0, 0);
		icons [1].rectTransform.anchoredPosition = new Vector2 (128, 0);
		icons [2].rectTransform.anchoredPosition = new Vector2 (-128, 0);
	}
}