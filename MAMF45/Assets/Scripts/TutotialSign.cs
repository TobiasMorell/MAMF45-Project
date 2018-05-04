using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutotialSign : MonoBehaviour {
	public Sprite[] TutorialSlides;
	public float TimeBetweenSlides;
	private Image _slideImage;

	private float _timer;
	private int _currentSlide = 0;

	void Start() {
		_slideImage = GetComponentInChildren<Image> ();
	}

	// Update is called once per frame
	void Update () {
		_timer += Time.deltaTime;

		if (_timer > TimeBetweenSlides) {
			_currentSlide = (_currentSlide + 1) % TutorialSlides.Length;
			_slideImage.sprite = TutorialSlides [_currentSlide];
			_timer = 0;
		}
	}
}
