﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoriaMovement : EnemyController {

	Rigidbody rb;
	public float move_speed = 0.5f;
	public float change_dir_time = 1f;
	public float reload_time = 1f;
	public float fire_delay = 5f;
	public Vector3 current_dir;
	public Rigidbody boomerang;
	public float boom_spawn_distance = 1f;
	public float throw_speed = 3f;
	public float return_speed = 1f;
        public bool isStunned = false;

	bool can_throw = true;
	int layer_mask = ~((1 << 8) | (1 << 9));
	Coroutine co;
	Animator anim;
	Rigidbody boom;

	Vector3[] directions = {
		new Vector3 (1, 0, 0),
		new Vector3 (0, 1, 0),
		new Vector3 (-1, 0, 0),
		new Vector3 (0, -1, 0)
	};

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody> ();
		co = StartCoroutine (Move ());
	}

	void Update (){
		Debug.DrawLine (transform.position, transform.position + current_dir * 1f);

		if (can_throw)
			StartCoroutine (ThrowBoomerang());
	}

	void FixedUpdate(){
        if(isStunned)
			return;
		if (Physics.Raycast (transform.position, current_dir, 0.51f, layer_mask)) {
			StopCoroutine (co);
			co = StartCoroutine (Move ());
		}
		rb.velocity = current_dir * move_speed;
	}

	void OnDestroy(){
		if (boom != null)
			Destroy (boom.gameObject);
	}

	IEnumerator Move()
	{		
		Vector3 direction;
		int iter = 0;
		while (true) {
			int i = Random.Range (0, 4);
			direction = directions [i];

			if (Physics.Raycast (transform.position, direction, 1f, layer_mask) == false) {
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

	IEnumerator ThrowBoomerang(){
		StopCoroutine (co);
		can_throw = false;
		yield return new WaitForSeconds (fire_delay);
		Debug.Log ("Goria throwing boomerang at " + current_dir.ToString());

		Vector3 throw_pos = transform.position;
		Vector3 throw_dir = current_dir;
		boom = Instantiate (boomerang, transform.position + throw_dir * boom_spawn_distance, transform.rotation);
		boom.velocity = throw_dir * throw_speed;

		int iter = 0;
		Vector3 orig_dir = current_dir;
		float orig_speed = move_speed;
		move_speed = 0f;
		while (Vector3.Distance(boom.position, transform.position) >= 0.25){
			StopCoroutine (co);
			current_dir = orig_dir;
			boom.velocity += (transform.position - boom.position) * return_speed;
			++iter;
			if (iter > 1000)
				break;

			yield return null;
		}

		Destroy (boom.gameObject);
		current_dir = orig_dir;

		move_speed = orig_speed;
		StartCoroutine (Move ());
		yield return new WaitForSeconds (reload_time);
		can_throw = true;
	}
}
