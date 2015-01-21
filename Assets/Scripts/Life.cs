using UnityEngine;
using System.Collections;

public class Life : TouchBehaviour {

	private int life = 3;
	public AudioClip sound;
	
	public int getLife()
	{
		return life;
	}
	public void setLife(int _life)
	{
		life = _life;
		this.guiText.text = string.Format("x {0}", _life);
	}

	public void setLifeWithSound(int _life)
	{
		audio.PlayOneShot(sound, 1.0f);
		life = _life;
		this.guiText.text = string.Format("x {0}", _life);
	}
}
