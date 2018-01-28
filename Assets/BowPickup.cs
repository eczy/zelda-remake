using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowPickup : MonoBehaviour {

	public AudioClip pickup_sound;

	void OnTriggerEnter(Collider coll){
		if (coll.GetComponent<ArrowKeyMovement> () != null) {
			AudioSource.PlayClipAtPoint (pickup_sound, Camera.main.transform.position);
			Destroy (gameObject);
		}
	}
}
