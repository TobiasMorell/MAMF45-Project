using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SicknessMaterialBlockProperty : MonoBehaviour {

	[SerializeField]
	private Color baseColor, sicknessColor;

	private float sicknessFade = 0;
	private bool isSick = false;

	new private Renderer renderer;
	private MaterialPropertyBlock propertyBlock;

	void Awake () {
		propertyBlock = new MaterialPropertyBlock ();
		renderer = GetComponent<Renderer> ();
	}

	void Update () {
		if (isSick && sicknessFade < 1) {
			sicknessFade = Mathf.Min(sicknessFade + Time.deltaTime, 1.0f);
		}
		else if (!isSick && sicknessFade > 0) {
			sicknessFade = Mathf.Min(sicknessFade - Time.deltaTime, 0.0f);
		}

		renderer.GetPropertyBlock(propertyBlock);
		propertyBlock.SetColor("_Color", Color.Lerp(baseColor, sicknessColor, sicknessFade));
		renderer.SetPropertyBlock(propertyBlock);
	}

	public void toggleSickness(bool isSick) {
		this.isSick = isSick;
	}
}
