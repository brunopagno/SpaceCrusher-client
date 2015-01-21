using UnityEngine;
using System.Collections;

public class ItemControl : TouchBehaviour {

    public GameObject communication;
    public int amount;

    private Vector2 startPosition;
    private bool started = false;

    public void DoActivate() {
        if (amount > 0) {
            communication.GetComponent<Communication>().SendChangedGun(this.gameObject.tag);
        }
    }

    public void DoSend() {
        if (amount > 0) {
            communication.GetComponent<Communication>().SendGun(this.gameObject.tag);
        }
    }

    void OnTouchBegin(Touch touch) {
        startPosition = Input.GetTouch(0).position;
        started = true;
    }

    void OnTouchEnd(Touch touch) {
        if (Vector2.Distance(startPosition, touch.position) > Screen.height / 4) {
            this.DoSend();
        } else {
            this.DoActivate();
        }
    }

}
