using UnityEngine;
using System.Collections;

public class RightButton : TouchButtonLogic {

    public GUITexture nave;
    public Texture normalTexture;
    public Texture pressedTexture;
    public GameObject communication;
    public float speed = 0.5f;

    void OnTouchBegan() {
        this.guiTexture.texture = pressedTexture;
    }

    void OnTouchEnded() {
        this.guiTexture.texture = normalTexture;
    }

    void OnTouchStationary() {
        float x = nave.transform.position.x + (speed * Time.deltaTime);
        if (x < 0)
            x = 0;
        nave.transform.position = new Vector3(x,
                                        nave.transform.position.y,
                                        nave.transform.position.z);
        communication.GetComponent<Communication>().SendPosition(nave.transform.position.x.ToString());
    }
}
