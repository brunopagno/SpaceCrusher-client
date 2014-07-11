﻿using UnityEngine;
using System.Collections;
using System;

public class TouchMove : MonoBehaviour {

	private Vector2 startPosition;
	bool start = false;
	public AudioClip sound;
	
	
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
			}
		}
		if (start) 
		{
			if (Input.GetTouch (0).phase == TouchPhase.Ended) 
			{
				Vector2 v2 = Input.GetTouch(0).position - startPosition;
				if(Vector2.Distance(startPosition, Input.GetTouch(0).position) > Screen.height / 4)
				{
					audio.PlayOneShot(sound, 1.0f);
				//	text.text = "acertou";
				}
				else
				{
				//	text.text = "errou";
				}
				start = false;
			}
		}
	}
}
