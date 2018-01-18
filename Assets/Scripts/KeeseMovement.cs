using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeeseMovement : MonoBehaviour {

	public float rest_time;
	public float fly_time;
	public float peak_speed;
	public float accel_time;

	bool can_fly = true;

	Rigidbody rb;
	Animator anim;

	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
		anim = GetComponent<Animator> ();
	}

	void Update ()
	{
		if (can_fly) {
			StartCoroutine (Fly ());
		}
	}

	IEnumerator Fly()
	{
		can_fly = false;
		rb.velocity = new Vector3 (0, 1, 0) * peak_speed;
		yield return new WaitForSeconds (fly_time);
		rb.velocity = Vector3.zero;
		yield return new WaitForSeconds (rest_time);
		can_fly = true;
	}
}
