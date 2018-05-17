using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rescale : MonoBehaviour {
	void Start () {
        Vector3 scaleTmp = transform.localScale;
        scaleTmp.x /= transform.parent.localScale.x;
        scaleTmp.y /= transform.parent.localScale.y;
        scaleTmp.z /= transform.parent.localScale.z;
        transform.localScale = scaleTmp;

        var obj = GetComponentInChildren<Billboard>();
        var pos = obj.transform.localPosition;
        obj.transform.localPosition = new Vector3(pos[0], 0.3f * transform.parent.localScale.y, pos[2]);
    }
}
