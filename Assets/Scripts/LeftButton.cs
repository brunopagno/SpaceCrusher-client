using UnityEngine;
using System.Collections;

public class LeftButton : TouchBehaviour {

    public Ship ship;

    void OnTouchBegin() {
        ship.MoveLeft = true;
    }

    void OnTouchEnd() {
        ship.MoveLeft = false;
    }

}
