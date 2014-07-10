using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

    public void SetScore(int score) {
        this.guiText.text = string.Format("Score: {0}", score);
    }

}
