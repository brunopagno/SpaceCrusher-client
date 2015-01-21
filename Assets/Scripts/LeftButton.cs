using UnityEngine;
using System.Collections;

public class LeftButton : TouchBehaviour {

    public Ship ship;

    public override void OnTouchBegin(Touch touch) {
        ship.MoveLeft = true;
    }

    public override void OnTouchEnd(Touch touch) {
        ship.MoveLeft = false;
    }

}
