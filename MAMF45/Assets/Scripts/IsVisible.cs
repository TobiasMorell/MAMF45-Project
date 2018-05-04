using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsVisible : MonoBehaviour {

	private bool isVisible;

	void OnBecameVisible()
	{
		isVisible = true;
	}

	void OnBecameInvisible()
	{
		isVisible = false;
	}

	public bool Visible()
	{
		return isVisible;
	}
}
