using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {

    public Communication communication;
    public float speed = 3;
    public bool MoveRight { get; set; }
    public bool MoveLeft { get; set; }

	void Update () {
        float movement = 0;
        if (MoveRight) movement += speed * Time.deltaTime;
        if (MoveLeft) movement -= speed * Time.deltaTime;

        transform.position = new Vector3(transform.position.x + movement,
                                         transform.position.y,
                                         transform.position.z);

        float pos = transform.position.x / Screen.width; // convert to percentual portion of screen before sending
        communication.GetComponent<Communication>().SendPosition(pos.ToString());
	}

}
