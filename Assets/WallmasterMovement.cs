using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallmasterMovement : EnemyController {

	Rigidbody rb;
	public float move_speed = 0.5f;
	public Transform[] move_points;
	public WallmasterController controller;

	bool holding_player = false;
	Rigidbody player;
	Vector3 current_dir;

	Vector3[] directions = {
		new Vector3 (1, 0, 0),
		new Vector3 (0, 1, 0),
		new Vector3 (-1, 0, 0),
		new Vector3 (0, -1, 0)
	};

	// Use this for initialization
	IEnumerator Start () {
		rb = GetComponent<Rigidbody> ();

		for (int i = 0; i < move_points.Length; ++i) {
			while (Mathf.Abs (Vector3.Magnitude (rb.position - move_points [i].position)) > 0.1) {
				rb.position += (move_points [i].position - rb.position).normalized * Time.deltaTime * move_speed;
				yield return null;
			}
			rb.position = move_points [i].position;
		}

		if (holding_player) {
			player.GetComponent<ArrowKeyMovement> ().Reset ();
			player.GetComponent<ArrowKeyMovement> ().enabled = true;
			player.GetComponent<Collider> ().enabled = true;
		}
		Destroy (gameObject);
	}

	void OnDestroy(){
		controller.WallmasterDied ();
	}


	void OnCollisionEnter(Collision coll)
	{
		if (coll.collider.gameObject.GetComponent<ArrowKeyMovement> () != null) {
			player = coll.collider.GetComponent<Rigidbody> ();
			player.transform.position = transform.position;
			GetComponent<SpriteRenderer> ().sortingOrder = 3;
			holding_player = true;
			StartCoroutine (CapturePlayer ());
		}
	}

	IEnumerator CapturePlayer(){
		while (true) {
			player.GetComponent<ArrowKeyMovement> ().enabled = false;
			player.GetComponent<Collider> ().enabled = false;
			player.position = rb.position;
			yield return null;
		}
	}
}