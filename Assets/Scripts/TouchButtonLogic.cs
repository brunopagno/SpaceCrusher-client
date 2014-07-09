using UnityEngine;
using System.Collections;

public class TouchButtonLogic : MonoBehaviour {
	
	void Update () 
	{
		if (Input.GetKey(KeyCode.Home) || Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Menu))
		{
			Application.Quit();
			return;
		}
		//is there a touch on screen?
		if (Input.touches.Length <= 0) 
		{
			//if no touhces then execute this code
		} else //if ther is a touch
		{ 
			//loop through all the touches on screen
			for(int i = 0; i < Input.touchCount; i++)
			{
				//executes this code for current touch on scren
				if(this.guiTexture != null && this.guiTexture.HitTest(Input.GetTouch(i).position))
				{
					//if current touch hits our guitexture, run this code
					if(Input.GetTouch(i).phase == TouchPhase.Stationary)
					{
						this.SendMessage("OnTouchBegan");
					}
					
					if(Input.GetTouch(i).phase == TouchPhase.Ended)
					{
						this.SendMessage("OnTouchEnded");
					}
					
					if(Input.GetTouch(i).phase == TouchPhase.Stationary)
					{
						this.SendMessage("OnTouchStationary");
					}
				}
			}
			
		}
	}
}
