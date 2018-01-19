using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeeseMovement : MonoBehaviour {

	public float rest_time;
	public float fly_time;
	public float peak_speed;
	public float accel_time;
	public float randomize_direction_time;

	bool can_fly = true;
	Vector3 vel;

	Rigidbody rb;
	Animator anim;

	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
		anim = GetComponent<Animator> ();
		anim.speed = 0;
	}

	void Update ()
	{
		if (can_fly) {
			StartCoroutine (Fly ());
		}
	}

	void OnCollisionEnter()
	{
		rb.velocity = -vel;
	}

	IEnumerator Fly()
	{
		can_fly = false;
		rb.velocity = new Vector3 (0, 1, 0) * peak_speed;
		float t = 0f;
		Debug.Log ("Accelerating");
		while (t < accel_time)
		{
			float frac_accel = t/accel_time;
			float frac_speed = Mathf.Lerp (0f, peak_speed, frac_accel);
			float frac_anim = Mathf.Lerp (0f, 1f, frac_accel);
			vel = new Vector3 (Random.Range (-1, 2), Random.Range (-1, 2), 0) * frac_speed;
			rb.velocity = vel;
			anim.speed = frac_anim;
			t += Time.deltaTime;

			float s = 0f;
			while (s < randomize_direction_time)
			{
				t += Time.deltaTime;
				s += Time.deltaTime;
				yield return null;
			}
		}

		t = 0f;
		Debug.Log ("Full speed");
		while (t < fly_time)
		{
			vel = new Vector3 (Random.Range (-1, 2), Random.Range (-1, 2), 0) * peak_speed;
			rb.velocity = vel;
			t += Time.deltaTime;

			float s = 0f;
			while (s < randomize_direction_time)
			{
				t += Time.deltaTime;
				s += Time.deltaTime;
				yield return null;
			}
		}

		t = 0f;
		Debug.Log ("Decelerating");
		while (t < accel_time)
		{
			float frac_accel = t/accel_time;
			float frac_speed = Mathf.Lerp (peak_speed, 0f, frac_accel);
			float frac_anim = Mathf.Lerp (1f, 0f, frac_accel);
			vel = new Vector3 (Random.Range (-1, 2), Random.Range (-1, 2), 0) * frac_speed;
			rb.velocity = vel;
			anim.speed = frac_anim;
			t += Time.deltaTime;

			float s = 0f;
			while (s < randomize_direction_time)
			{
				t += Time.deltaTime;
				s += Time.deltaTime;
				yield return null;
			}
		}

		rb.velocity = Vector3.zero;
		anim.speed = 0f;

		yield return new WaitForSeconds (rest_time);
		can_fly = true;
	}
}
