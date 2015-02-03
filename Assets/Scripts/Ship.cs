using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {

    public Communication communication;
    public Sprite shipSprite;
    public Sprite bombSprite;

    private float originalSpeed;
    public float speed = 3;
    public bool MoveRight { get; set; }
    public bool MoveLeft { get; set; }

    private float _height;
    private float _width;
    private Color originalColor;

    void Start() {
        _height = Camera.main.orthographicSize * 2;
        _width = _height * Camera.main.aspect;
        originalSpeed = speed;
    }

    public void SetShipSprite()
    {
        this.GetComponent<SpriteRenderer>().sprite = shipSprite;
    }

    public void SetBombSprite()
    {
        this.GetComponent<SpriteRenderer>().sprite = bombSprite;
    }

    public void SetColor(Color _color)
    {
        this.GetComponent<SpriteRenderer>().color = _color;
    }

    public void SetOriginalColor(Color _color)
    {
        this.GetComponent<SpriteRenderer>().color = _color;
        originalColor = _color;
    }

    public void SetOriginalColor()
    {
        this.GetComponent<SpriteRenderer>().color = originalColor;
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

    internal void SpeedReduction(bool p) {
        if (p) {
            originalSpeed = speed;
            speed = speed / 2;
        } else {
            speed = originalSpeed;
        }
    }
}
