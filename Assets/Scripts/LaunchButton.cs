using UnityEngine;

public class LaunchButton : TouchBehaviour
{
    public GameObject ControllerObjects;
    public GameObject Ship;
    public GameObject Communication;

    public void Start()
    {
        this.gameObject.SetActive(false);
    }

    public override void OnTouchEnd(Touch touch)
    {
        //enviar sinalização para o server que lançou bomba

        this.gameObject.SetActive(false);
        ResetTouch();
        ControllerObjects.SetActive(true);
        ResetTouch();


        Ship.GetComponent<Ship>().SetShipSprite();
        Ship.GetComponent<Ship>().SetOriginalColor();

        Communication.GetComponent<Communication>().LaunchBomb();
    }
}
