using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour {

	public AudioClip rupee_collection_sound_clip;

	Inventory inventory;

	void Start ()
	{
		// Try to grab a reference to the inventory component on this gameobject.
		inventory = GetComponent<Inventory> ();
		if (inventory == null)
			Debug.LogWarning ("WARNING: Gameobject with a collector has no inventory!");
	}

	void OnTriggerEnter (Collider coll)
	{
		GameObject object_collided_with = coll.gameObject;

		if (object_collided_with.tag == "rupee")
		{
			// Check to see if inventory exists before adding rupee to it.
			if (inventory != null)
				inventory.AddRupees (1);
			Destroy (object_collided_with);

			// Play sound effect
			AudioSource.PlayClipAtPoint (rupee_collection_sound_clip, Camera.main.transform.position);
		}
	}
}
