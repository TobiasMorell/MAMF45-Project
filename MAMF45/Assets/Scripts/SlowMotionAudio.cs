using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotionAudio : MonoBehaviour {
	
	public void Update() {
		var audios = GetComponents<AudioSource>();
		foreach (var audio in audios)
			audio.pitch = Time.timeScale;
	}
}
