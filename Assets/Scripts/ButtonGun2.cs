using UnityEngine;
using System.Collections;

public class ButtonGun2 : TouchButtonLogic {
	public GUIText text;
	public GameObject communication;
	void OnTouchEnded()
	{
		communication.GetComponent<Communication>().SendPosition ("red");
		text.text = "Red";
	}

    protected override void AtUpdateEnd() {
        // nothing
    }
}
