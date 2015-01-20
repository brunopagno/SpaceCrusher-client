using UnityEngine;
using System.Collections;

public class RightButton : TouchButtonLogic {

    public Ship ship;

    void OnTouchBegan() {
        ship.MoveRight = true;
    }

    void OnTouchEnded() {
        ship.MoveRight = false;
    }

}