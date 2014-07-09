using UnityEngine;
using System.Collections;

public class ButtonLightning : TouchButtonLogic {

	public Texture2D oneNotSelected;
	public Texture2D oneSelected;
	public Texture2D twoNotSelected;
	public Texture2D twoSelected;
	public Texture2D threeNotSelected;
	public Texture2D threeSelected;
	public float time = 5.0f;
	public GameObject roulet;
	public GameObject communication;
	
	public GUITexture texture1;
	public GUITexture texture3;
	public GUITexture texture2;
	private int turn = 1;
	private float countTime = 0.0f;
	private bool active = false;
	public GUIText text;
	private int count = 1;
	void Start() 
	{
		roulet.SetActive (false);
	}

	void OnTouchEnded()
	{
		text.text = "especial";
		ActiveRoulet ();
	}
	
	public void ActiveRoulet()
	{
		roulet.SetActive (true);
		time = 5.0f;
		countTime = 0.0f;
		active = true;
		count = 1;
	}
	
	void Update() 
	{
		if (active) 
		{
			countTime += Time.deltaTime;
			time -= Time.deltaTime;
			if(count == 4)
			{
				active = false;
				roulet.SetActive(false);
			}
			else if (time <= 0.0f) 
			{
				time = 5.0f;
				countTime = 0.0f;
				count++;
				communication.GetComponent<Communication>().SendRouletResult (turn.ToString());
			} 
			else 
			{
				if (countTime >= 1.0f)
				{
					switch (turn)
					{
						case 1: 
								texture1.texture = oneSelected;
								texture3.texture = threeNotSelected;
								turn++;
								break;
						case 2:
								texture2.texture = twoSelected;
								texture1.texture = oneNotSelected;
								turn++;
								break;
						case 3:
								texture3.texture = threeSelected;
								texture2.texture = twoNotSelected;
								turn = 1;
								break;
					}
					countTime = 0.0f;
				}
			}
		}
	}
}
