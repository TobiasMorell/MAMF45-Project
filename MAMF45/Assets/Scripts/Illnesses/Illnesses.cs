using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum IllnessTypes {
	Cold, Pneumenia, Clamydia, Death
}
public class Illnesses : MonoBehaviour {
	private static Dictionary<IllnessTypes, IllnessAsset> _illnessDetails;

	// Use this for initialization
	static void Init () {
		var illnesses = Resources.LoadAll<IllnessAsset> ("Illnesses");
		Debug.Log ("Found " + illnesses.Count() + " illnesses");
		_illnessDetails = illnesses
			.Select ((arg) => new KeyValuePair<IllnessTypes, IllnessAsset> (arg.Type, arg))
			.ToDictionary (ks => ks.Key, es => es.Value);
	}

	public static IllnessAsset GetDetails(IllnessTypes type) {
		if (_illnessDetails == null)
			Init ();
		return _illnessDetails [type];
	}
}
