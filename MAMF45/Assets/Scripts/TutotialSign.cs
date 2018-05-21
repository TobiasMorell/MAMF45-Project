using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

public class TutotialSign : MonoBehaviour {
	public Sprite[] TutorialSlides;
	public float TimeBetweenSlides;
    public bool AdvanceManually;
	private Image _slideImage;

	private float _timer;
	private int _currentSlide = 0;
    private Hand _leftHand;
    private Hand _rightHand;
    private bool _buttonPressed;

    void Start() {
		_slideImage = GetComponentInChildren<Image> ();
        var player = GameObject.Find("Player").GetComponent<Player>();
        _leftHand = player.leftHand;
        _rightHand = player.rightHand;
	}

	// Update is called once per frame
	void Update () {
		_timer += Time.deltaTime;

		if (!AdvanceManually && _timer > TimeBetweenSlides) {
			_currentSlide = (_currentSlide + 1) % TutorialSlides.Length;
			_slideImage.sprite = TutorialSlides [_currentSlide];
			_timer = 0;
		}


        if (AdvanceManually) {
            if (!_leftHand || !_rightHand) {
                var player = GameObject.Find("Player").GetComponent<Player>();
                _leftHand = player.leftHand;
                _rightHand = player.rightHand;
            }
            else if (_leftHand.GetStandardInteractionButton() || _rightHand.GetStandardInteractionButton()) {
                if (!_buttonPressed) {
                    _currentSlide = (_currentSlide + 1) % TutorialSlides.Length;
                    _slideImage.sprite = TutorialSlides[_currentSlide];
                    _buttonPressed = true;
                }
            }
            else
                _buttonPressed = false;
        }
	}
}
