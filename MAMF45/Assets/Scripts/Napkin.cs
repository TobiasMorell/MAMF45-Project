using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Napkin : MonoBehaviour {

	public Color ColorClean = new Color(1f, 0.43f, 0);
	public Color ColorDirty = new Color(0.52f, 0.47f, 0f);

	private MaterialPropertyBlock propertyBlock;
	new private Renderer renderer;

	private int useCount;

	void Start() {
		propertyBlock = new MaterialPropertyBlock ();
		renderer = GetComponent<Renderer> ();
	}

	public void Use() {
		useCount += 1;

		renderer.GetPropertyBlock(propertyBlock);
		propertyBlock.SetColor("_Color", ColorDirty);
		renderer.SetPropertyBlock(propertyBlock);
	}

	public bool SpreadsDisease() {
		if (useCount > 0 && Random.Range (0, 1f) > 0.5f) {
			return true;
		}
		return false;
	}
}
