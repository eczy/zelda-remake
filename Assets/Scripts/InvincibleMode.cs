using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibleMode : MonoBehaviour {

	public bool invincible = false;

	Inventory inventory;

	void Start ()
	{
		// Try to grab a reference to the inventory component on this gameobject.
		inventory = GetComponent<Inventory> ();
		if (inventory == null)
			Debug.LogWarning ("WARNING: Player has no inventory!");
	}
	
	// Update is called once per frame
	void Update () {

		if (invincible == false)
			return;
		
		int num_rupees = inventory.GetRupees ();
		int num_bombs = inventory.GetBombs ();
		int num_keys = inventory.GetKeys ();

		if (num_rupees != 255) {
			inventory.AddRupees (255 - num_rupees);
		}
		if (num_bombs != 16) {
			inventory.AddBombs (16 - num_bombs);
		}
		if (num_keys != 255) {
			inventory.AddKeys (255 - num_keys);
		}

		// TODO: health management
	}
}
