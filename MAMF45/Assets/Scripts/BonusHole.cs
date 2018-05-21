using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusHole : MonoBehaviour {
	public Text scoreText;
	public AudioClip audioClip;

	private float cooldown = 0;

	private void Update()
	{
		if (cooldown > 0)
			cooldown -= Time.deltaTime;
	}

	private void OnTriggerEnter(Collider other) {
		if (cooldown <= 0)
		{
			cooldown = 1;
			ScoreBoard.Instance.BonusHole(scoreText);
			GetComponent<AudioSource>().PlayOneShot(audioClip);
			Destroy(other.gameObject);
		}
	}
}
