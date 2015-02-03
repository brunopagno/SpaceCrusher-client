using UnityEngine;
using System.Collections;

public abstract class TouchBehaviour : MonoBehaviour {

    private bool began = false;
    private int fingerId = -1;

    virtual public void OnTouchBegin(Touch touch) { }
    virtual public void OnTouchStay(Touch touch) { }
    virtual public void OnTouchMove(Touch touch) { }
    virtual public void OnTouchEnd(Touch touch) { }

    public void ResetTouch()
    {
        this.began = false;
        this.fingerId = -1;
    }

    void Update() {
        foreach (Touch touch in Input.touches) {
            if (GetComponent<BoxCollider2D>().OverlapPoint(Camera.main.ScreenToWorldPoint(touch.position))) {
                if (touch.phase == TouchPhase.Began) {
                    OnTouchBegin(touch);
                    began = true;
                    fingerId = touch.fingerId;
                }
            }
            if (began && fingerId == touch.fingerId) {
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
