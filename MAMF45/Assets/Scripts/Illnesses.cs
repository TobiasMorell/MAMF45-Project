using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum IllnessTypes {
	Cold, Pneumenia, Clamydia
}
public class Illnesses : MonoBehaviour {
	private Dictionary<IllnessTypes, IllnessAsset> _illnessDetails;
	private static Illnesses Instance;

	// Use this for initialization
	void Start () {
		var illnesses = Resources.LoadAll<IllnessAsset> ("Illnesses");
		Debug.Log ("Found " + illnesses.Count() + " illnesses");
		_illnessDetails = illnesses
			.Select ((arg) => new KeyValuePair<IllnessTypes, IllnessAsset> (arg.Type, arg))
			.ToDictionary (ks => ks.Key, es => es.Value);

		Instance = this;
	}

	public static IllnessAsset GetDetails(IllnessTypes type) {
		return Instance._illnessDetails [type];
	}
}
