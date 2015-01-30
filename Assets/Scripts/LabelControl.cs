using UnityEngine;
using System.Collections;

public class LabelControl : MonoBehaviour {

    public GUIText label;
    private int amount;
    public int Amount {
        get { return this.amount; }
        set {
            this.amount = value;
            this.label.text = amount.ToString() + "x";
        }
    }

}
