using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rescale : MonoBehaviour {
    private Vector3 uiPosition;
    private Billboard billboard;

	void Start () {
        Vector3 scaleTmp = transform.localScale;
        scaleTmp.x /= transform.parent.localScale.x;
        scaleTmp.y /= transform.parent.localScale.y;
        scaleTmp.z /= transform.parent.localScale.z;
        transform.localScale = scaleTmp;

        billboard = GetComponentInChildren<Billboard>();

        var pos = billboard.transform.localPosition;
        uiPosition = new Vector3(pos[0], 0.3f * transform.parent.localScale.y, pos[2]);
    }

    void Update() {
        billboard.transform.localPosition = uiPosition;
    }
}
