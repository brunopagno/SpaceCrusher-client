using UnityEngine;
using System.Collections;
using System.Threading;

public class ButtonLightning : TouchButtonLogic {

    public Texture2D originalRoulet;
    public Texture2D oneSelected;
    public Texture2D twoSelected;
    public Texture2D threeSelected;
    public Texture2D fourSelected;
    public float setTime = 3.0f;
    public GameObject roulet;
    public GameObject communication;
    public GameObject buttonRotate;
    public GameObject bullets;
    private float time;
    
    //public GUITexture texture;
    private int turn = 1;
    private float countTime = 0.0f;
    private bool active = false;
    private int count = 1;
    public float rotateSpeed = 10;
	private bool findNumber = false;
	private int rouletResul;
	private float timeToDisable = 1.0f;

	public GameObject nave;
	public GameObject nave2;
	public GameObject leftButton;
	public GameObject rightButton;

    private bool goGoGo = false;
    public bool GoGoGo {
        get { return this.goGoGo; }
    }

    void Start() {
        bullets.GetComponent<Bullets>().setBullets(0);
        roulet.SetActive(false);
        buttonRotate.SetActive(false);
		findNumber = false;
    }

    void OnTouchEnded() {
        ActiveRoulet();
    }

    public void ActiveRoulet() {
        if (bullets.GetComponent<Bullets>().getBullets() > 0) {
            communication.GetComponent<Communication>().SendChangedGun(this.gameObject.tag);
            roulet.guiTexture.texture = originalRoulet;
            roulet.SetActive(true);
            buttonRotate.SetActive(true);
            time = setTime;
            countTime = 0.0f;
            //active = true;
            turn = 1;
            count = 1;
            goGoGo = true;
			findNumber = false;
			System.Random rnd = new System.Random();
			rouletResul = rnd.Next(1, 5);
			setScreenObjectsActive(false);
        }
    }

	private void setScreenObjectsActive(bool _active)
	{
		nave.SetActive(_active);
		nave2.SetActive(_active);
		leftButton.SetActive(_active);
		rightButton.SetActive(_active);

	}

    public void RotateRoulet() {
        active = true;
        buttonRotate.SetActive(false);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (goGoGo) {
                RotateRoulet();
            }
        }
        if (count == 4) {
			timeToDisable -= Time.deltaTime;
			if(timeToDisable <= 0.0f)
			{
			//	Thread.Sleep(1000);
				setScreenObjectsActive(true);
	            active = false;
	            roulet.SetActive(false);
	            buttonRotate.SetActive(false);
	            goGoGo = false;
				timeToDisable = 1.0f;
				communication.GetComponent<Communication>().SendChangedGun("gun1");
			}
			else return;
        }
        if (active) {
            //text.text = "entrou";
            countTime += Time.deltaTime;
            time -= Time.deltaTime;
            if (findNumber) {
				System.Random rnd = new System.Random();
				rouletResul = rnd.Next(1, 5);
                turn = 1;
                time = setTime;
                countTime = 0.0f;
                count++;
                active = false;
                communication.GetComponent<Communication>().SendRouletResult(turn.ToString());
				if(count != 4)
                	buttonRotate.SetActive(true);
				findNumber = false;
            } else {
                if (countTime >= 0.1f) {
                    switch (turn) {
                        case 1:
                            roulet.guiTexture.texture = oneSelected;
							if (time <= 0.0f && turn == rouletResul) {
								findNumber = true;
							}
							else
                            	turn++;
                            break;
                        case 2:
                            roulet.guiTexture.texture = twoSelected;
							if (time <= 0.0f && turn == rouletResul) {
								findNumber = true;
							}
							else
								turn++;
                            break;
                        case 3:
                            roulet.guiTexture.texture = threeSelected;
							if (time <= 0.0f && turn == rouletResul) {
								findNumber = true;
							}
							else
								turn++;
							break;
                        case 4:
                            roulet.guiTexture.texture = fourSelected;
							if (time <= 0.0f && turn == rouletResul) {
								findNumber = true;
							}
							else
								turn = 1;
							break;	
                        default: break;
                    }
                    countTime = 0.0f;
                }
            }
        }
    }
}
