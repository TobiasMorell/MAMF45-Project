using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnySound : MonoBehaviour {

	public AudioClip[] audioClips;

	public AudioClip sneezeBeginAudio;
	public AudioClip[] sneezeAudio;
	public AudioClip deathAudio;

	private AudioSource idleAudioSource;
	private AudioSource effectAudioSource;
	
	void Awake () {
		var audioSources = GetComponents<AudioSource>();
		idleAudioSource = audioSources[0];
		effectAudioSource = audioSources[1];
		idleAudioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
		idleAudioSource.Play();
	}
	
	public void PlaySneezeBeginSound() {
		effectAudioSource.PlayOneShot(sneezeBeginAudio);
	}
	
	public void PlaySneezeSound() {
		effectAudioSource.PlayOneShot(sneezeAudio[Random.Range(0, sneezeAudio.Length)]);
	}

	public void PlayDeathSound()
	{
		effectAudioSource.PlayOneShot(deathAudio);
	}
}
