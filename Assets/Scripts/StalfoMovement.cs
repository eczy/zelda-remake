using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalfoMovement : MonoBehaviour {

	Rigidbody rb;
	public float move_time = 0.5f;

	Vector3 current_dir;

	Vector3[] directions = {
		new Vector3 (1, 0, 0),
		new Vector3 (0, 1, 0),
		new Vector3 (-1, 0, 0),
		new Vector3 (0, -1, 0)
	};

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		StartCoroutine (Move ());
	}

	void Update (){
		Debug.DrawLine (transform.position, transform.position + current_dir * 1f);
	}

	IEnumerator Move()
	{
		Vector3 direction;
		while (true) {
			int i = Random.Range (0, 4);
			direction = directions [i];
			if (!Physics.Raycast (transform.position, direction, 1f) && direction != -current_dir) {
				current_dir = direction;
				break;
			}
		}

		float t = 0f;
		Vector3 start = rb.position;
		Vector3 end = rb.position + current_dir;
		while (t < move_time) {
			t += Time.deltaTime;
			float progress = t / move_time;
			rb.position = Vector3.Lerp (start, end, progress);
			yield return null;
		}
		StartCoroutine (Move ());
	}
}