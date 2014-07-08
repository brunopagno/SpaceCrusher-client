using UnityEngine;
using System.Collections;

public class MoveButton : TouchButtonLogic {

    public GUITexture nave;
    public Texture normalTexture;
    public Texture pressedTexture;
    public GameObject communication;
    public float speed = 0.5f;
    public int operation = 0;

    private bool positionChange = false;
    private float ttime = 0;

    void OnTouchBegan() {
        this.guiTexture.texture = pressedTexture;
    }

    void OnTouchEnded() {
        this.guiTexture.texture = normalTexture;
    }

    void OnTouchStationary() {
        positionChange = true;

        float x = 0;
        if (operation == 0) {
            x = nave.transform.position.x + (speed * Time.deltaTime);
            if (x > 1)
                x = 1;
        } else {
            x = nave.transform.position.x - (speed * Time.deltaTime);
            if (x < 0)
                x = 0;
        }
        nave.transform.position = new Vector3(x,
                                        nave.transform.position.y,
                                        nave.transform.position.z);
    }

    protected override void AtUpdateEnd() {
        ttime += Time.deltaTime;
        if (positionChange && ttime > 0.2f) {
            positionChange = false;
            communication.GetComponent<Communication>().SendPosition(nave.transform.position.x.ToString());
        }
    }
}
