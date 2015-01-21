using UnityEngine;
using System.Collections;

public class RightButton : TouchBehaviour {

    public Ship ship;

    void OnTouchBegin() {
        ship.MoveRight = true;
    }

    void OnTouchEnd() {
        ship.MoveRight = false;
    }

}
