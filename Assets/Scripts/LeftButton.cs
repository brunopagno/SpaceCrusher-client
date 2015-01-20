using UnityEngine;
using System.Collections;

public class LeftButton : TouchButtonLogic {

    public Ship ship;

    void OnTouchBegan() {
        ship.MoveLeft = true;
    }

    void OnTouchEnded() {
        ship.MoveLeft = false;
    }
}
