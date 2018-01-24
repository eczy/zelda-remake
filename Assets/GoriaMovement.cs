using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoriaMovement : EnemyController {

	Rigidbody rb;
	public float move_speed = 0.5f;
	public float change_dir_time = 1f;
	public float reload_time = 1f;
	public float fire_delay = 5f;
	public Vector3 current_dir;

	bool can_throw;
	int layer_mask = ~((1 << 8) | (1 << 9));
	Coroutine co;
	Animator anim;

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

		if (can_throw)
			StartCoroutine (ThrowBoomerang);
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
		Vector3 temp = current_dir;
		current_dir = Vector3.zero;
		anim.speed = 0;

		yield return new WaitForSeconds (fire_delay);


	}
}