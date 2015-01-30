using UnityEngine;
using System.Collections;

public class BombButton : ItemControl {

    public override void OnTouchEnd(Touch touch) {
        this.DoActivate();
        this.DoActivateBomb();
    }

    private void DoActivateBomb() {
        /* ativa o modo bomba */
        // esconder botões
        // mudar textura da elipse para bomba (catar imagem no google =D)
        // adicionar botão de ~lançar bomba~ (copia a forma que os outros botoes foram feitos)
        
        // quando a bomba for lançada, esconder botão de lançar bomba e adicionar de volta os outros que foram removidos
    }

}
