using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour {

	public AudioClip rupee_collection_sound_clip;

	Inventory inventory;

	void Start(){
		inventory = GetComponent<Inventory> ();
		if (inventory == null) {
			Debug.LogWarning ("WARNING: GameObject with a collector has no inventory to store things in!");
		}
	}

	void OnTriggerEnter(Collider coll){
		GameObject object_collided_with = coll.gameObject;

		if (object_collided_with.tag == "rupee") {
			if (inventory != null) {
				inventory.AddRupees (1);
			}
			Destroy (object_collided_with);

			//play sound effect
			AudioSource.PlayClipAtPoint(rupee_collection_sound_clip,Camera.main.transform.position);
		}
	}
}
