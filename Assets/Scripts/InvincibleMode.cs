using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibleMode : MonoBehaviour {

	public bool invincible = false;
	public float toggle_delay = 5.0f;

	Inventory inventory;
	Health health;

	int orig_rupees, orig_bombs, orig_keys;
	float orig_health;

	void Start ()
	{
		// Try to grab a reference to the inventory component on this gameobject.
		inventory = GetComponent<Inventory> ();
		if (inventory == null)
			Debug.LogWarning ("WARNING: Player has no inventory!");

		// Try to grab a reference to the health component on this gameobject.
		health = GetComponent<Health> ();
		if (health == null)
			Debug.LogWarning ("WARNING: Player has no health!");
	}

	void SetInvincible ()
	{
		Debug.Log ("Bestowing invincibility");
		orig_rupees = inventory.GetRupees ();
		orig_bombs = inventory.GetBombs ();
		orig_keys = inventory.GetKeys ();

		orig_health = health.GetHealth ();

		invincible = true;
	}

	void RemoveInvincible ()
	{
		Debug.Log ("Removing invincibility");
		inventory.SetRupees (orig_rupees);
		inventory.SetBombs (orig_bombs);
		inventory.SetKeys (orig_keys);

		health.SetHealth (orig_health);

		invincible = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Alpha1) && !invincible)
			StartCoroutine ("SetInvincible");
		else if (Input.GetKeyDown(KeyCode.Alpha1) && invincible)
			StartCoroutine ("RemoveInvincible");

		if (invincible == false)
			return;
		
		if (inventory.GetRupees() != 255)
			inventory.SetRupees (255);

		if (inventory.GetBombs() != 16)
			inventory.SetBombs (16);
		
		if (inventory.GetKeys() != 255)
			inventory.SetKeys (255);

		if (health.GetHealth () < health.max_health)
			health.SetHealth (health.max_health);
	}
}
