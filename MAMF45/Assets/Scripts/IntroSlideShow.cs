using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSlideShow : MonoBehaviour {

    private Transform player;

	// Use this for initialization
	void Start () {
        player = Camera.main.transform;
    }
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.Euler(0,player.rotation.eulerAngles.y,0);
	}
}
