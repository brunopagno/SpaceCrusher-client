using UnityEngine;
using System.Collections;

public class RotateButton : TouchButtonLogic {

	public GameObject buttonLightning;
	public bool canTouch = true;
	public GUIText text;

	void OnTouchEnded() 
	{
		text.text = "rotate";
		buttonLightning.GetComponent<ButtonLightning> ().RotateRoulet ();
		//this.gameObject.SetActive (false);
	}
}
