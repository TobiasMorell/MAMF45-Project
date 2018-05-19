using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnySound : MonoBehaviour {

	public AudioClip[] audioClips;

	private AudioSource audioSource;
	
	void Awake () {
		audioSource = GetComponent<AudioSource>();
		audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
		audioSource.Play();
	}
	
}
