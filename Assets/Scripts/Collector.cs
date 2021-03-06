﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour {

	public AudioClip rupee_collection_sound_clip;
	public AudioClip heart_collection_sound_clip;

	public Inventory inventory;
	Health health = null;
	Health_Boss bhealth = null;

	void Start ()
	{
		// Try to grab a reference to the health component on this gameobject.
		bhealth = GetComponent<Health_Boss>();
		
		health = GetComponent<Health> ();
	}

	void OnTriggerEnter (Collider coll)
	{
		GameObject object_collided_with = coll.gameObject;

		if (object_collided_with.tag == "rupee") {
			// Check to see if inventory exists before adding rupee to it.
			if (inventory != null)
				inventory.AddRupees (1);
			Destroy (object_collided_with);

			// Play sound effect
			AudioSource.PlayClipAtPoint (rupee_collection_sound_clip, Camera.main.transform.position);
		} else if (object_collided_with.tag == "heart") {
			if (health != null)
				health.Heal (1);
			if (bhealth != null)
				bhealth.Heal (1);
			AudioSource.PlayClipAtPoint (heart_collection_sound_clip, Camera.main.transform.position);
			Destroy (object_collided_with);
		} else if (object_collided_with.tag == "key") {
			if (inventory != null)
				inventory.AddKeys (1);
			AudioSource.PlayClipAtPoint (heart_collection_sound_clip, Camera.main.transform.position);
			Destroy (object_collided_with);
		} else if (object_collided_with.tag == "bomb") {
			if (inventory != null)
				inventory.AddBombs (1);
			Destroy (object_collided_with);
		}
	}
}
