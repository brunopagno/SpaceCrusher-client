﻿using UnityEngine;
using System.Collections;

public class ItemControl : TouchBehaviour {

    public GameObject communication;
    public GUIText label;
    private int amount;
    public int Amount {
        get { return this.amount; }
        set {
            this.amount = value;
            this.label.text = amount.ToString() + "x";
        }
    }

    public bool DoActivate() {
        if (amount > 0) {
            communication.GetComponent<Communication>().SyncChangedItem(this.gameObject.tag);
            return true;
        }
        return false;
    }

    public override void OnTouchBegin(Touch touch) {
        this.DoActivate();
    }
}
