using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnySound : MonoBehaviour {

	public AudioClip[] audioClips;

	public AudioClip sneezeBeginAudio;
	public AudioClip[] sneezeAudio;
	public AudioClip deathAudio;
	public AudioClip[] moveAudio;
	public AudioClip loveAudio;
	public AudioClip happyAudio;

	private AudioSource idleAudioSource;
	private AudioSource loudEffectAudioSource;
	private AudioSource silentEffectAudioSource;

	void Awake () {
		var audioSources = GetComponents<AudioSource>();
		idleAudioSource = audioSources[0];
		loudEffectAudioSource = audioSources[1];
		silentEffectAudioSource = audioSources[2];
		idleAudioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
		idleAudioSource.Play();
	}
	
	public void PlaySneezeBeginSound() {
		loudEffectAudioSource.PlayOneShot(sneezeBeginAudio);
	}
	
	public void PlaySneezeSound() {
		loudEffectAudioSource.PlayOneShot(sneezeAudio[Random.Range(0, sneezeAudio.Length)]);
	}
	
	public void PlayDeathSound() {
		loudEffectAudioSource.PlayOneShot(deathAudio);
	}
	
	public void PlayMoveSound() {
		silentEffectAudioSource.PlayOneShot(moveAudio[Random.Range(0, moveAudio.Length)]);
	}

	public void PlayLoveSound()	{
		silentEffectAudioSource.PlayOneShot(loveAudio);
	}

	public void PlayHappySound()	{
		silentEffectAudioSource.PlayOneShot(happyAudio);
	}
}
