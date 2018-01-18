using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StalfoMovement : MonoBehaviour {

    public float movement_speed = 3f;
	public Vector3 direction = new Vector3 (1, 0, 0);
	public float randomize_direction_delay = 5.0f;

    Rigidbody rb;

	void Start () {
        rb = GetComponent<Rigidbody>();
		StartCoroutine (RandomizeDirection ());
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        rb.velocity = direction * movement_speed;
		Debug.Log (rb.velocity.ToString ());
		Vector3 current_pos = transform.position;
		Debug.Log (direction.ToString ());
		if (Mathf.Abs (direction.y) < 0.1) {
			transform.position = new Vector3 (current_pos.x, Mathf.RoundToInt (current_pos.y), current_pos.z);
		} else {
			transform.position = new Vector3 (Mathf.RoundToInt (current_pos.x), current_pos.y, current_pos.z);
		}
	}

	void OnCollisionEnter(Collision coll)
	{
		GameObject other = coll.collider.gameObject;

		Vector3 dir = transform.position - other.transform.position;
		if (Mathf.Abs (dir.x) > Mathf.Abs (dir.y)) {
			if (dir.x >= 0)
				dir = new Vector3 (1, 0, 0);
			else
				dir = new Vector3 (-1, 0, 0);
		} else {
			if (dir.y >= 0)
				dir = new Vector3 (0, 1, 0);
			else
				dir = new Vector3 (0, -1, 0);
		}

		Vector3 current_pos = transform.position;
		transform.position = new Vector3 (Mathf.RoundToInt(current_pos.x), Mathf.RoundToInt (current_pos.y), current_pos.z);

		int r = UnityEngine.Random.Range (0, 3);

		switch (r) {
		case (0):
			direction = Quaternion.Euler (0, 0, 90) * dir;
			break;
		case (1):
			direction = Quaternion.Euler (0, 0, 180) * dir;
			break;
		case (2):
			direction = Quaternion.Euler (0, 0, 270) * dir;
			break;
		default:
			direction = Quaternion.Euler (0, 0, 180) * dir;
			break;
		}

		//rb.AddForce (direction * movement_speed, ForceMode.Impulse);
	}

	IEnumerator RandomizeDirection () {
		yield return new WaitForSeconds (randomize_direction_delay);
		Debug.Log ("Randomizing direction!");

		int r = UnityEngine.Random.Range (0, 3);

		switch (r) {
		case (0):
			direction = Quaternion.Euler (0, 0, 90) * direction;
			break;
		case (1):
			direction = Quaternion.Euler (0, 0, 180) * direction;
			break;
		case (2):
			direction = Quaternion.Euler (0, 0, 270) * direction;
			break;
		default:
			direction = Quaternion.Euler (0, 0, 180) * direction;
			break;
		}

		StartCoroutine (RandomizeDirection ());
		yield return null;
	}
}
