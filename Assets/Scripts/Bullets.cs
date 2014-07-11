using UnityEngine;
using System.Collections;

public class Bullets : MonoBehaviour {

	private int bullets = 1;
	public AudioClip sound;

	public int getBullets()
	{
		return bullets;
	}
	public void setBullets(int _bullets)
	{
		bullets = _bullets;
		this.guiText.text = string.Format("x {0}", bullets);
	}

	public void setBulletsWithSound(int _bullets)
	{
		audio.PlayOneShot(sound, 1.0f);
		bullets = _bullets;
		this.guiText.text = string.Format("x {0}", bullets);
	}
}
