using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {
    public float speed = 4;

    void Update() {
        float horizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(horizontal, 0, 0);
    }
}
