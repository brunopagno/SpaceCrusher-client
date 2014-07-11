using UnityEngine;
using System.Collections;

public class RotateButton : TouchButtonLogic {

	public GameObject buttonLightning;
	public bool canTouch = true;

	void OnTouchEnded() 
	{
		buttonLightning.GetComponent<ButtonLightning> ().RotateRoulet ();
		//this.gameObject.SetActive (false);
	}
}
