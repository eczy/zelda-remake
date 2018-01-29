using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLockOnEnter : MonoBehaviour {

	public Door door;

	void OnTriggerEnter(Collider coll){
		if (coll.GetComponent<ArrowKeyMovement> () != null) {
			Debug.Log ("Locking door behind player!!!");
			door.Lock ();
			Destroy (this);
		}
	}
}
