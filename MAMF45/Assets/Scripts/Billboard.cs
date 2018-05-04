using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using System;
using System.Collections.Generic;

public class Billboard : MonoBehaviour {
	public Sprite HealthyIcon;
	public Image[] icons;

	void Start() {
		Vector3 scaleTmp = transform.localScale;
		scaleTmp.x /= transform.parent.localScale.x;
		scaleTmp.y /= transform.parent.localScale.y;
		scaleTmp.z /= transform.parent.localScale.z;
		transform.localScale = scaleTmp;
	}

	void Update()
	{
		Camera cam = Camera.main;
		transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward,
			cam.transform.rotation * Vector3.up);
	}

	public void ClearDiseases() {
		Array.ForEach (icons, i => Clear (i));
	}

	private void Clear(Image image) {
		image.enabled = false;
	}

	public void DisplayHealthy() {
		ClearDiseases ();
		SetupForThreeImages ();
		Display (icons [1], HealthyIcon);
	}

	private void RedrawIcons(IllnessAsset[] illnesses) {
		var count = 0;
		for (var i = 0; i < illnesses.Length; i++) {
			count++;
			Display (icons[i], illnesses[i].Icon);
		}

		if (count == 2)
			SetupForTwoImages ();
		else
			SetupForThreeImages ();
	}

	public void DisplayDiseases(params IllnessAsset[] illnesses) {
		ClearDiseases ();
		RedrawIcons (illnesses);
	}

	private void Display(Image image, Sprite icon) {
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
		icons [1].rectTransform.anchoredPosition = new Vector2 (-128, 0);
	}
}