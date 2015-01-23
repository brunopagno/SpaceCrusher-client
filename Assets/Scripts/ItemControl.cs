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
            communication.GetComponent<Communication>().SyncChangedItem(this.gameObject.tag);
        }
    }

    public override void OnTouchBegin(Touch touch) {
        startPosition = Input.GetTouch(0).position;
    }

    public override void OnTouchEnd(Touch touch) {
        this.DoActivate();
    }

}
