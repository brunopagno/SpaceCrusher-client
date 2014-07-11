using UnityEngine;
using System.Collections;

public class TouchLife : MonoBehaviour {

	public GameObject communication;
	public GameObject life;
	private Vector2 startPosition;
	bool start = false;
	public AudioClip sound;
	
	public void ExecutActivate() {
		//communication.GetComponent<Communication>().RPCOut (this.gameObject.tag);
		if (life.GetComponent<Life>().getLife() > 1) {
			communication.GetComponent<Communication>().SendLife();
			audio.PlayOneShot(sound, 1.0f);
		}
	}
	
	void Update () 
	{
		//is there a touch on screen?
		if (Input.touches.Length <= 0) 
		{
			//if no touhces then execute this code
		} else //if ther is a touch
		{
			if(this.guiTexture != null && this.guiTexture.HitTest(Input.GetTouch(0).position))
			{
				//if current touch hits our guitexture, run this code
				if(Input.GetTouch(0).phase == TouchPhase.Began)
				{
					startPosition = Input.GetTouch(0).position;
					start = true;
				}
				if(Input.GetTouch(0).phase == TouchPhase.Ended)
				{
				}
			}
		}
		if (start) 
		{
			if (Input.GetTouch (0).phase == TouchPhase.Ended) 
			{
				Vector2 v2 = Input.GetTouch(0).position - startPosition;
				if(Vector2.Distance(startPosition, Input.GetTouch(0).position) > Screen.height / 4)
				{
					this.ExecutActivate();
				}
				start = false;
			}
		}
	}
}
