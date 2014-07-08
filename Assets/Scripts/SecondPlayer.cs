using UnityEngine;
using System.Collections;

public class SecondPlayer : MonoBehaviour {

	public void MoveSecondPlayer(string _position)
	{
		float x;
		float.TryParse(_position, out x);
		this.gameObject.transform.position = new Vector3 (x, 
                                                  this.gameObject.transform.position.y, 
                                                  this.gameObject.transform.position.z);
	}
}
