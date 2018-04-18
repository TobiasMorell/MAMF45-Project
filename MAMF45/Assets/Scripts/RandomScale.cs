using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomScale : MonoBehaviour {
    
	void Awake () {
        transform.localScale *= Random.value + 1;
        transform.localScale += new Vector3(Random.value / 3, Random.value, Random.value / 3);
	}
}
