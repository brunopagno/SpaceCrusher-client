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
        if (x > 1)
            x = 1;
        communication.GetComponent<Communication>().SendPosition(x.ToString());
        nave.transform.position = new Vector3(x,
                                        nave.transform.position.y,
                                        nave.transform.position.z);
    }
}
