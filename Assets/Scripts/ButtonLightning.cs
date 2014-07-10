using UnityEngine;
using System.Collections;

public class ButtonLightning : TouchButtonLogic {

    public Texture2D originalRoulet;
    public Texture2D oneSelected;
    public Texture2D twoSelected;
    public Texture2D threeSelected;
    public Texture2D fourSelected;
    public float setTime = 10.0f;
    public GameObject roulet;
    public GameObject communication;
    public GameObject buttonRotate;
    public GameObject bullets;
    private float time;
    
    //public GUITexture texture;
    private int turn = 1;
    private float countTime = 0.0f;
    private bool active = false;
    public GUIText text;
    private int count = 1;
    public float rotateSpeed = 10;

    private bool goGoGo = false;
    public bool GoGoGo {
        get { return this.goGoGo; }
    }

    void Start() {
        bullets.GetComponent<Bullets>().setBullets(0);
        roulet.SetActive(false);
        buttonRotate.SetActive(false);
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
        }
    }

    public void RotateRoulet() {
        active = true;
        buttonRotate.SetActive(false);
    }

    void Update() {
        if (count == 4) {
            active = false;
            roulet.SetActive(false);
            buttonRotate.SetActive(false);
            goGoGo = false;
        }
        if (active) {
            //text.text = "entrou";
            countTime += Time.deltaTime * rotateSpeed;
            time -= Time.deltaTime * rotateSpeed;
            if (time <= 0.0f) {
                turn = 1;
                time = setTime;
                countTime = 0.0f;
                count++;
                active = false;
                communication.GetComponent<Communication>().SendRouletResult(turn.ToString());
                buttonRotate.SetActive(true);
            } else {
                if (countTime >= 1.0f) {
                    switch (turn) {
                        case 1:
                            roulet.guiTexture.texture = oneSelected;
                            turn++;
                            break;
                        case 2:
                            roulet.guiTexture.texture = twoSelected;
                            turn++;
                            break;
                        case 3:
                            roulet.guiTexture.texture = threeSelected;
                            turn++;
                            break;
                        case 4:
                            roulet.guiTexture.texture = fourSelected;
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
