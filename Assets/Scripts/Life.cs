using UnityEngine;
using System.Collections;

public class Life : MonoBehaviour {

    public Communication comm;
    public GUIText label;
    private int amount = 0;
    bool startVibration = false;

    public int Amount {
        get { return this.amount; }
        set {
            if (value < amount && comm.vibractionActive) {
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
