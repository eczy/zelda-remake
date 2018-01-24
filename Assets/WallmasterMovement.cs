﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallmasterMovement : EnemyController {

	Rigidbody rb;
	public float move_speed = 0.5f;
	public float change_dir_time = 1f;

	Vector3 current_dir;
	int layer_mask = ~((1 << 8) | (1 << 9) | (1<<10) );
	Coroutine co;

	Vector3[] directions = {
		new Vector3 (1, 0, 0),
		new Vector3 (0, 1, 0),
		new Vector3 (-1, 0, 0),
		new Vector3 (0, -1, 0)
	};

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		co = StartCoroutine (Move ());
	}

	void Update (){
		Debug.DrawLine (transform.position, transform.position + current_dir * 1f);
	}

	void FixedUpdate(){
		if (Physics.Raycast (transform.position, current_dir, 0.51f, layer_mask)) {
			StopCoroutine (co);
			co = StartCoroutine (Move ());
		}
		rb.velocity = current_dir * move_speed;
	}

	IEnumerator Move()
	{
		Vector3 direction;
		int iter = 0;
		while (true) {
			int i = Random.Range (0, 4);
			direction = directions [i];

			if (Physics.Raycast (transform.position, direction, 0.51f, layer_mask) == false) {
				current_dir = direction;
				break;
			}

			// Prevent infinite looping
			iter++;
			if (iter > 100)
				break;
		}
		yield return new WaitForSeconds (change_dir_time);
		co = StartCoroutine (Move ());
	}

	void OnTriggerExit(Collider coll){
		if (coll.gameObject.GetComponent<WallmasterController> () != null) {
			coll.GetComponent<WallmasterController> ().num_wallmasters--;
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter(Collision coll)
	{
		
		if (coll.collider.gameObject.GetComponent<ArrowKeyMovement> () != null) {
			coll.collider.transform.parent = transform;
			coll.collider.GetComponent<ArrowKeyMovement> ().enabled = false;
			GetComponent<SpriteRenderer> ().sortingOrder = 3;
			coll.collider.transform.position = transform.position;
			StopCoroutine (co);
		}
	}
}