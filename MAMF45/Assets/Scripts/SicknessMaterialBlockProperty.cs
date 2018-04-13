using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SicknessMaterialBlockProperty : MonoBehaviour {

	[SerializeField]
	private Color color1, color2;

	public float fade = 0;

	new private Renderer renderer;
	private MaterialPropertyBlock propertyBlock;

	void Awake () {
		propertyBlock = new MaterialPropertyBlock ();
		renderer = GetComponent<Renderer> ();
	}

	void Update () {
		renderer.GetPropertyBlock(propertyBlock);
		propertyBlock.SetColor("_Color", Color.Lerp(color1, color2, fade));
		renderer.SetPropertyBlock(propertyBlock);
	}
}
