using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultTablet : Destructible {
    void OnTriggerExit(Collider other) {
        if (other.CompareTag(Tags.FENCE)) {
            OnDestroyed.Invoke();
        }
    }
}
