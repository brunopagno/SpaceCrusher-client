using UnityEngine;
using System.Collections;

public class ItemControl : TouchBehaviour {

    private Vector2 startPosition;

    public GameObject communication;
    public GUIText label;
    private int amount;
    public int Amount {
        get { return this.amount; }
        set {
            this.amount = value;
            this.label.text = amount.ToString();
        }
    }

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

    public override void OnTouchBegin(Touch touch) {
        startPosition = Input.GetTouch(0).position;
    }

    public override void OnTouchEnd(Touch touch) {
        if (Vector2.Distance(startPosition, touch.position) > Screen.height / 4) {
            this.DoSend();
        } else {
            this.DoActivate();
        }
    }

}
