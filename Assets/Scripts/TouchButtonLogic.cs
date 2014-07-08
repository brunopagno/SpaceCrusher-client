using UnityEngine;
using System.Collections;

public abstract class TouchButtonLogic : MonoBehaviour {

    void Update() {
        if (Input.GetKey(KeyCode.Home) || Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Menu)) {
            Application.Quit();
            return;
        }
        if (Input.touches.Length <= 0) {
        } else {
            for (int i = 0; i < Input.touchCount; i++) {
                if (this.guiTexture != null && this.guiTexture.HitTest(Input.GetTouch(i).position)) {
                    if (Input.GetTouch(i).phase == TouchPhase.Stationary) {
                        this.SendMessage("OnTouchBegan");
                    }
                    if (Input.GetTouch(i).phase == TouchPhase.Ended) {
                        this.SendMessage("OnTouchEnded");
                    }
                    if (Input.GetTouch(i).phase == TouchPhase.Stationary) {
                        this.SendMessage("OnTouchStationary");
                    }
                }
            }
        }
        AtUpdateEnd();
    }

    protected abstract void AtUpdateEnd();
}
