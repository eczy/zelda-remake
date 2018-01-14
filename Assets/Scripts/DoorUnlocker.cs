using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorUnlocker : MonoBehaviour {

	Inventory inventory;

	void Start ()
	{
		// Try to grab a reference to the inventory component on this gameobject.
		inventory = GetComponent<Inventory> ();
		if (inventory == null)
			Debug.LogWarning ("WARNING: Gameobject with a door unlocker has no inventory!");
	}

	void OnTriggerEnter (Collider coll)
	{
		GameObject object_collided_with = coll.gameObject;
		Door door = object_collided_with.GetComponent<Door> ();

		if (door == null || door.locked == false)
			return;

		int keys = inventory.GetKeys ();
		if (keys > 0) {
			door.Unlock ();
			inventory.SetKeys (keys - 1);
		}
	}

}
