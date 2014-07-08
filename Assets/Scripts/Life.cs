using UnityEngine;
using System.Collections;

public class Life : MonoBehaviour {

	public void setLife(int _life)
	{
		this.guiText.text = string.Format("x {0}", _life);
	}
}
