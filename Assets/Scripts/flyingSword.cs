﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyingSword : MonoBehaviour {

	public float sword_speed;
	public float spawn_distance;
	public Rigidbody sword_up;
	public Rigidbody sword_down;
	public Rigidbody sword_left;
	public Rigidbody sword_right;
	public Sprite shoot_up_sprite;
	public Sprite shoot_down_sprite;
	public Sprite shoot_left_sprite;
	public Sprite shoot_right_sprite;
	public float reload_time = 1.0f;
	public float control_delay = 0.25f;

	ArrowKeyMovement controller;
	Inventory inventory;
	SpriteRenderer renderer;
	Health playerHealth;
	Animator animator;
	bool reloading = false;

	// Use this for initialization
	void Start () {
		controller = GetComponent<ArrowKeyMovement> ();
		inventory = GetComponent<Inventory> ();
		renderer = GetComponent<SpriteRenderer> ();
		animator = GetComponent<Animator> ();
		playerHealth = GetComponent<Health> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.X) && !reloading && (playerHealth.GetHealth()==playerHealth.max_health)) {
			StartCoroutine(Shoot ());
		}
	}

	IEnumerator Shoot ()
	{
		int rupees = inventory.GetRupees ();
		if (rupees == 0) {
			Debug.Log ("Cannot shoot swords with no rupees");
			yield break;
		}

		controller.DisableControls ();
		reloading = true;

		inventory.SetRupees (rupees - 1);
		Vector3 forward = controller.Forward ();
		Transform transform = GetComponent<Transform> ();

		Rigidbody sword;
		Sprite original_sprite = renderer.sprite;
		animator.enabled = false;
		if (forward.x > 0) {
			sword = sword_right;
			renderer.sprite = shoot_right_sprite;
		} else if (forward.x < 0) {
			sword = sword_left;
			renderer.sprite = shoot_left_sprite;
		} else if (forward.y > 0) {
			sword = sword_up;
			renderer.sprite = shoot_up_sprite;
		} else if (forward.y < 0) {
			sword = sword_down;
			renderer.sprite = shoot_down_sprite;
		} else {
			sword = sword_up;
		}
		
		Rigidbody spawned_sword = Instantiate (sword, transform.position + forward * spawn_distance, transform.rotation);
		spawned_sword.velocity = forward * sword_speed;

		StartCoroutine(Reload ());
		yield return new WaitForSeconds(control_delay);
		renderer.sprite = original_sprite;
		animator.enabled = true;
		controller.EnableControls ();
	}

	IEnumerator Reload (){
		yield return new WaitForSeconds (reload_time);
		reloading = false;
	}
}
