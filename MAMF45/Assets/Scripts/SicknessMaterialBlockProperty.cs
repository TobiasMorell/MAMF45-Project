using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SicknessMaterialBlockProperty : MonoBehaviour {
	#pragma warning disable 0649
	[SerializeField]
	private Color baseColor1, baseColor2, baseColor3, baseColor4;
	#pragma warning restore 0649

	private Color defaultColor;
	private float defaultColorFraction;

	private Color startColor;
	private Color targetColor;
	private Color currentColor;

	private float fadeTimer = 0;

	new private Renderer renderer;
	private MaterialPropertyBlock propertyBlock;

	void Awake () {
        var c1 = Color.Lerp(baseColor1, baseColor2, Random.value);
        var c2 = Color.Lerp(baseColor3, baseColor4, Random.value);
		defaultColor = Color.Lerp(c1, c2, Random.value);
		defaultColorFraction = 0.7f;

		startColor = currentColor = targetColor = defaultColor;

        propertyBlock = new MaterialPropertyBlock ();
		renderer = GetComponent<Renderer> ();
	}

	void Update () {
		fadeTimer = Mathf.Min(fadeTimer + Time.deltaTime, 1.0f);

		currentColor = Color.Lerp (startColor, targetColor, fadeTimer);

		renderer.GetPropertyBlock(propertyBlock);
		propertyBlock.SetColor("_Color", Color.Lerp(defaultColor, currentColor, defaultColorFraction));
		renderer.SetPropertyBlock(propertyBlock);
	}

	public void InterpolateTo(Color color) {
		targetColor = color;
		startColor = currentColor;
		fadeTimer = 0;
	}

	public void InterpolateToDefault() {
		InterpolateTo (defaultColor);
	}

	public void SetDefaultColorFraction(float fraction) {
		defaultColorFraction = fraction;
	}
}
