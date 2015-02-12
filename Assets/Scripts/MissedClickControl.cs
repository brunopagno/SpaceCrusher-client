using UnityEngine;
using System.Collections;

public class MissedClickControl : TouchBehaviour {

    public GameObject communication;
    public TouchBehaviour[] other;

    public override void OnTouchBegin(Touch touch) {
        int c = counter;
        foreach (TouchBehaviour tb in other) {
            c -= tb.counter;
        }
        communication.GetComponent<Communication>().MissedClicks(name + "," + c);
    }

}
