using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour {

	public AudioClip rupee_collection_sound_clip;

	Inventory inventory;
	Health health;

	void Start ()
	{
		// Try to grab a reference to the inventory component on this gameobject.
		inventory = GetComponent<Inventory> ();
		if (inventory == null)
			Debug.LogWarning ("WARNING: Gameobject with a collector has no inventory!");

		// Try to grab a reference to the health component on this gameobject.
		health = GetComponent<Health> ();
		if (health == null)
			Debug.LogWarning ("WARNING: Gameobject with collector has no health!");
	}

	void OnTriggerEnter (Collider coll)
	{
		GameObject object_collided_with = coll.gameObject;

		if (object_collided_with.tag == "rupee") {
			// Check to see if inventory exists before adding rupee to it.
			if (inventory != null)
				inventory.AddRupees (1);

			// Play sound effect
			AudioSource.PlayClipAtPoint (rupee_collection_sound_clip, Camera.main.transform.position);
		} else if (object_collided_with.tag == "heart") {
			if (health != null)
				health.Heal (1);
		} else if (object_collided_with.tag == "key") {
			if (inventory != null)
				inventory.AddKeys (1);
		}
		Destroy (object_collided_with);
	}
}
