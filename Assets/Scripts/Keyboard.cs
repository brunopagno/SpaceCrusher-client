using UnityEngine;
using System.Collections;

public class Keyboard : MonoBehaviour {

    public float speed = 0.5f;
    public ButtonGun gun2;
    public ButtonGun gun3;
    public ButtonLightning lightning;
    public TouchLife life;

    void Update() {
        float transH = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(transH, 0, 0);
        if (transform.position.x < 0) {
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
        }
        if (transform.position.x > 1) {
            transform.position = new Vector3(1, transform.position.y, transform.position.z);
        }

        if (Input.GetKeyDown(KeyCode.Z)) {
            gun2.ExecutActivate();
        }
        if (Input.GetKeyDown(KeyCode.X)) {
            gun3.ExecutActivate();
        }
        if (Input.GetKeyDown(KeyCode.C)) {
            if (!lightning.GoGoGo) {
                lightning.ActiveRoulet();
            } else {
                lightning.RotateRoulet();
            }
        }

        if (Input.GetKeyDown(KeyCode.V)) {
            gun2.ExecuteSend();
        }
        if (Input.GetKeyDown(KeyCode.B)) {
            gun3.ExecuteSend();
        }
        if (Input.GetKeyDown(KeyCode.N)) {
            life.ExecutActivate();
        }

        GameObject commGO = GameObject.Find("Communication");
        Communication comm = commGO.GetComponent<Communication>();
        if (comm.IsActive) {
            comm.SendPosition(transform.position.x.ToString());
        }
    }
}
