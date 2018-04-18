using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SicknessMaterialBlockProperty : MonoBehaviour {

	[SerializeField]
	private Color baseColor1, baseColor2, baseColor3, baseColor4, sicknessColor;

    private Color color;
	private float sicknessFade = 0;
	private bool isSick = false;

	new private Renderer renderer;
	private MaterialPropertyBlock propertyBlock;

	void Awake () {
        var c1 = Color.Lerp(baseColor1, baseColor2, Random.value);
        var c2 = Color.Lerp(baseColor3, baseColor4, Random.value);
        color = Color.Lerp(c1, c2, Random.value);
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
		propertyBlock.SetColor("_Color", Color.Lerp(color, sicknessColor, sicknessFade));
		renderer.SetPropertyBlock(propertyBlock);
	}

	public void ToggleSickness(bool isSick) {
		this.isSick = isSick;
	}
}
