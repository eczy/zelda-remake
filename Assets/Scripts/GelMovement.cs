using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GelMovement : MonoBehaviour {

	Rigidbody rb;
	public float max_move_delay = 1f;
	public float min_move_delay = 0.1f;
	public float move_time = 0.5f;

	int layer_mask = (1 << 8) | (1 << 9);

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

	IEnumerator Move()
	{
		Vector3 direction;
		int iter = 0;
		while (true) {
			int i = Random.Range (0, 4);
			direction = directions [i];
			if (!Physics.Raycast (transform.position, direction, layer_mask))
				break;

			if (iter > 100)
				break;
		}
			
		float move_delay = Random.Range (min_move_delay, max_move_delay);
		yield return new WaitForSeconds (move_delay);

		float t = 0f;
		Vector3 start = rb.position;
		Vector3 end = rb.position + direction;
		while (t < move_time) {
			t += Time.deltaTime;
			float progress = t / move_time;
			rb.position = Vector3.Lerp (start, end, progress);
			yield return null;
		}
		StartCoroutine (Move ());
	}
}
