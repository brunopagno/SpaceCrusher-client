using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {

    public Communication communication;
    public float speed = 3;
    public bool MoveRight { get; set; }
    public bool MoveLeft { get; set; }

    private float _height;
    private float _width;

    void Start() {
        _height = Camera.main.orthographicSize * 2;
        _width = _height * Camera.main.aspect;
    }

    void Update() {
        float movement = 0;
        if (MoveRight) movement += speed * Time.deltaTime;
        if (MoveLeft) movement -= speed * Time.deltaTime;

        float result = transform.position.x + movement;
        if (result > _width / 2) {
            result = _width / 2;
        }
        if (result < -_width / 2) {
            result = -_width / 2;
        }

        transform.position = new Vector3(result, transform.position.y, transform.position.z);

        if (communication.connected) {
            float pos = transform.position.x / _width + .5f; // convert to percentual portion of screen before sending
            communication.GetComponent<Communication>().SyncPosition(pos.ToString());
        }
    }

}
