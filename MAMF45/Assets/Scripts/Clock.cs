using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour {

	public Slider ClockSlider;

	private bool running;
	private float timer;
	private float maxTime;
	
	void Update () {
		if (running) {
			timer = Mathf.Min(timer + Time.deltaTime, maxTime);
			ClockSlider.value = timer / maxTime;
		}
	}

	public void Run(float time) {
		timer = 0;
		maxTime = time;
		running = true;
	}
}
