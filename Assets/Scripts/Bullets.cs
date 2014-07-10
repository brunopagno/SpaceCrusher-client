using UnityEngine;
using System.Collections;

public class Bullets : MonoBehaviour {

	private int bullets = 0;

	public int getBullets()
	{
		return bullets;
	}
	public void setBullets(int _bullets)
	{
		bullets = _bullets;
		this.guiText.text = string.Format("x {0}", bullets);
	}
}
