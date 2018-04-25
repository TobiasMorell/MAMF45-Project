using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using System;

public class Billboard : MonoBehaviour {
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

	public void Clear(Image image) {
		image.enabled = false;
	}

	public void AddDisease(params IllnessAsset[] illnesses) {
		for (var i = 0; i < illnesses.Length; i++) {
			Display (icons[i], illnesses[i]);
		}

		if (illnesses.Length == 2)
			SetupForTwoImages ();
		else
			SetupForThreeImages ();
	}

	public void Display(Image image, IllnessAsset illness) {
		image.sprite = illness.Icon;
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