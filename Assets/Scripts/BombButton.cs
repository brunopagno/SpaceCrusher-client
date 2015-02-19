using UnityEngine;
using System.Collections;

public class BombButton : ItemControl {

    public GameObject ControllerObjects;
    public GameObject Ship;
    public GameObject LaunchButton;

    public override void OnTouchBegin(Touch touch) {
        if (this.DoActivate()) {
            this.DoActivateBomb();
        }
    }

    private void DoActivateBomb() {
        /* ativa o modo bomba */
        // esconder botões
        // When controller objects are set to false the game cannot update the amount of bombs,
        // so this is an allowed gambi =D
        this.Amount--;
        ControllerObjects.SetActive(false);

        // mudar textura da elipse para bomba (catar imagem no google =D)
        Ship.GetComponent<Ship>().SetBombSprite();
        Ship.GetComponent<Ship>().SetColor(Color.white);

        // adicionar botão de ~lançar bomba~ (copia a forma que os outros botoes foram feitos)
        LaunchButton.SetActive(true);

        ResetTouch();
    }
}
