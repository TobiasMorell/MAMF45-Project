using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixDrawerPosition : MonoBehaviour {

    private float maxX;
    private float minX;

	void Start ()
    {
        maxX = transform.localPosition.x + 0.118f;
        minX = transform.localPosition.x - 0.6f;
        Debug.Log(maxX + " < > " + minX);
    }
	
	void Update () {
        if (transform.localPosition.x < minX)
        {
            transform.localPosition = new Vector3(minX, transform.localPosition.y, transform.localPosition.z);
            Debug.Log("MIN HIT!");
        }
        if (transform.localPosition.x > maxX)
        {
            Debug.Log("MAX HIT!");
            transform.localPosition = new Vector3(maxX, transform.localPosition.y, transform.localPosition.z);
        }
    }
}
