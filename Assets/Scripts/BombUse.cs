using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombUse : Weapon {

	public Bomb bomb;
	public float spawn_distance = 1f;
	public float reload_time = 2f;
	public AudioClip bomb_set_sound;

	bool can_set_bomb = true;
	ArrowKeyMovement controller;
	Inventory inventory;

	void Start ()
	{
		controller = GetComponent<ArrowKeyMovement> ();
		inventory = GetComponent<Inventory> ();
	}
	
	// Update is called once per frame
	void Update () {
		// TODO: implement weapon switcher
		if (Input.GetKeyDown(usage_key) && can_set_bomb){
			int num_bombs = inventory.GetBombs ();
			if (num_bombs <= 0)
				return;

			inventory.SetBombs (num_bombs - 1);
			StartCoroutine (SetBomb ());
		}
	}

	IEnumerator SetBomb()
	{
		can_set_bomb = false;
		Vector3 forward = controller.Forward();

		Bomb spawned_bomb = Instantiate (bomb, transform.position + forward * spawn_distance, transform.rotation);
		AudioSource.PlayClipAtPoint (bomb_set_sound, Camera.main.transform.position);
		yield return new WaitForSeconds (reload_time);
		can_set_bomb = true;
	}
}
