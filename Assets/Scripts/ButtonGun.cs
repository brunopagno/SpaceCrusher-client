using UnityEngine;
using System.Collections;

public class ButtonGun : MonoBehaviour {//TouchButtonLogic {
    public GameObject communication;
    public GameObject bullets;
	private Vector2 startPosition;
	bool start = false;
	public AudioClip sound;

    void OnTouchEnded() {
        this.ExecutActivate();
    }

    public void ExecutActivate() {
        //communication.GetComponent<Communication>().RPCOut (this.gameObject.tag);
        if (bullets.GetComponent<Bullets>().getBullets() > 0) {
            communication.GetComponent<Communication>().SendChangedGun(this.gameObject.tag);
        }
    }

    public void ExecuteSend() {
        if (bullets.GetComponent<Bullets>().getBullets() > 0) {
            audio.PlayOneShot(sound, 1.0f);
            communication.GetComponent<Communication>().SendGun(this.gameObject.tag);
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
					this.ExecutActivate();
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
                    this.ExecuteSend();
				}
				start = false;
			}
		}
	}

}
