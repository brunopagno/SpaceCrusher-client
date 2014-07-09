using UnityEngine;
using System.Collections;

public class Keyboard : MonoBehaviour {

    public float speed = 0.5f;

	void Update () {
        float transH = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(transH, 0, 0);
        if (transform.position.x < 0) {
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
        }
        if (transform.position.x > 1) {
            transform.position = new Vector3(1, transform.position.y, transform.position.z);
        }
	}
}
