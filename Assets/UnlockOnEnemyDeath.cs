using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockOnEnemyDeath : MonoBehaviour {

	public int num_alive;
	Door door;

	void Start(){
		door = GetComponent<Door> ();
	}
	void Update(){
		if (num_alive == 0) {
			Debug.Log ("All dead!");
			door.Unlock ();
		}
	}
}