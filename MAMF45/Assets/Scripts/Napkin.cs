using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Napkin : MonoBehaviour {

	public Color ColorClean = new Color(1f, 0.43f, 0);
	public Color ColorDirty = new Color(0.52f, 0.47f, 0f);

	private MaterialPropertyBlock propertyBlock;
	new private Renderer renderer;

	private Dictionary<Type, int> usedIllnesses;

	void Start() {
		propertyBlock = new MaterialPropertyBlock ();
		renderer = GetComponent<Renderer> ();
		usedIllnesses = new Dictionary<Type, int> ();
	}

	public void Use(Illness[] illnesses) {
		foreach (var illness in illnesses) {
			int value;
			usedIllnesses.TryGetValue (typeof(Illness), out value);
			usedIllnesses.Add (typeof(Illness), value + 1);
		}

		renderer.GetPropertyBlock(propertyBlock);
		propertyBlock.SetColor("_Color", ColorDirty);
		renderer.SetPropertyBlock(propertyBlock);
	}

	public Illness[] SpreadsDisease() {
		List<Illness> illnesses = new List<Illness> ();
		foreach (var entry in usedIllnesses) {
			if (UnityEngine.Random.Range (0, 1f) <= GetChance (entry.Value)) {
				illnesses.Add ((Illness)Activator.CreateInstance (entry.Key));
			}
		}
		return illnesses.ToArray();
	}

	private float GetChance(int number) {
		return 1 - 1 / (number + 1f);
	}
}
