using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearOnEnemyKill : MonoBehaviour {

	public int num_alive;

	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer> ().enabled = false;
		GetComponent<Collider> ().enabled = false;
	}

	void Update(){
		if (num_alive == 0) {
			Debug.Log ("All dead!");
			GetComponent<SpriteRenderer> ().enabled = true;
			GetComponent<Collider> ().enabled = true;
		}
	}
}
