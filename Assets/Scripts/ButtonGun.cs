using UnityEngine;
using System.Collections;

public class ButtonGun : TouchButtonLogic {
    public GUIText text;
    public GameObject communication;
    public GameObject bullets;

    void OnTouchEnded() {
        this.ExecutActivate();
    }

    public void ExecutActivate() {
        //communication.GetComponent<Communication>().RPCOut (this.gameObject.tag);
        if (bullets.GetComponent<Bullets>().getBullets() > 0 || this.gameObject.tag != "gun1") {
            communication.GetComponent<Communication>().SendChangedGun(this.gameObject.tag);
            text.text = this.gameObject.tag;
        }
    }

}
