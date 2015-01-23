using UnityEngine;
using System.Collections;

public abstract class TouchBehaviour : MonoBehaviour {

    private bool began = false;

    virtual public void OnTouchBegin(Touch touch) { }
    virtual public void OnTouchStay(Touch touch) { }
    virtual public void OnTouchMove(Touch touch) { }
    virtual public void OnTouchEnd(Touch touch) { }

    void Update() {
        foreach (Touch touch in Input.touches) {
            if (GetComponent<BoxCollider2D>().OverlapPoint(Camera.main.ScreenToWorldPoint(touch.position))) {
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
