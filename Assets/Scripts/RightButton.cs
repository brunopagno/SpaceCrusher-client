using UnityEngine;
using System.Collections;

public class RightButton : TouchBehaviour {

    public Ship ship;

    public override void OnTouchBegin(Touch touch) {
        ship.MoveRight = true;
    }

    public override void OnTouchEnd(Touch touch) {
        ship.MoveRight = false;
    }

}
