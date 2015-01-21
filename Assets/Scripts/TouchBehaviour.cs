using UnityEngine;
using System.Collections;

public abstract class TouchBehaviour : MonoBehaviour {

    private bool began = false;

    void OnTouchBegin(Touch touch) { }
    void OnTouchStay(Touch touch) { }
    void OnTouchMove(Touch touch) { }
    void OnTouchEnd(Touch touch) { }

    void Update() {
        foreach (Touch touch in Input.touches) {
            if (GetComponent<BoxCollider2D>().OverlapPoint(touch.position)) {
                if (touch.phase == TouchPhase.Began) {
                    OnTouchBegin(touch);
                    began = true;
                }
            }
            if (began) {
                switch (touch.phase) {
                    case TouchPhase.Stationary:
                        OnTouchStay(touch);
                        break;
                    case TouchPhase.Moved:
                        OnTouchMove(touch);
                        break;
                    case TouchPhase.Ended:
                    case TouchPhase.Canceled:
                        OnTouchEnd(touch);
                        break;
                }
            }
        }
    }

}
