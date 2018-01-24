using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GelMovement : EnemyController {

	Rigidbody rb;
	public float max_move_delay = 1f;
	public float min_move_delay = 0.1f;
	public float move_time = 0.5f;
	float move_speed;

	Vector3 current_dir;
	int layer_mask = ~((1 << 8) | (1 << 9));
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
		move_speed = 1 / move_time;
	}

	void Update (){
		Debug.DrawLine (transform.position, transform.position + current_dir * 1f);
	}

	IEnumerator Move()
	{
		Vector3 direction;
		int iter = 0;
		while (true) {
			int i = Random.Range (0, 4);
			direction = directions [i];
			if (direction == -current_dir)
				continue;

			if (Physics.Raycast (transform.position, direction, 1f, layer_mask) == false) {
				current_dir = direction;
				break;
			}

			// Prevent infinite looping
			iter++;
			if (iter > 100)
				break;
		}

		yield return new WaitForSeconds (Random.Range(min_move_delay, max_move_delay));
		rb.velocity = current_dir * move_speed;
		yield return new WaitForSeconds (move_time);
		rb.velocity = Vector3.zero;
		co = StartCoroutine (Move ());
	}
}
