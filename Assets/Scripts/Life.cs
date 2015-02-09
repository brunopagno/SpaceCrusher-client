using UnityEngine;
using System.Collections;

public class Life : MonoBehaviour {

    public GUIText label;
    private int amount = 0;
    bool startVibration = false;
    public int Amount {
        get { return this.amount; }
        set {
            if (value < amount) {
                startVibration = true;
            }
            this.amount = value;
            this.label.text = amount.ToString() + "x";
        }
    }

    void Update() {
        if (startVibration) {
            Handheld.Vibrate();
            startVibration = false;
        }
    }

}
