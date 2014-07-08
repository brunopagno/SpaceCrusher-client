using UnityEngine;
using System.Collections;

public class ButtonGun : TouchButtonLogic {
	public GUIText text;
	public GameObject communication;

	void OnTouchEnded()
	{
		//communication.GetComponent<Communication>().RPCOut (this.gameObject.tag);
		communication.GetComponent<Communication>().SendChangedGun (this.gameObject.tag);
		text.text = this.gameObject.tag;
	}

    protected override void AtUpdateEnd() {
        // nothing
    }
}
