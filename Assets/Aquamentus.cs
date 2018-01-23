using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aquamentus : MonoBehaviour {

	public float max_reload_time = 3f;
	public float min_reload_time = 0.5f;
	public float power_up_time = 0.5f;
	public Transform fireball_spawnpoint;
	public float leftlim = -2;
	public float rightlim = 2;
	public Rigidbody fireball;
	public float fireball_speed = 2f;

	bool canshoot = true;
	Animator anim;
	Rigidbody rb;
	float startposx;

	Vector3[] shootdirs = {
		new Vector3 (-3, 1, 0).normalized,
		new Vector3 (-3, -1, 0).normalized,
		new Vector3 (-1, 0, 0)
	};

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody> ();
		startposx = transform.position.x;
		StartCoroutine (Move ());
	}
	
	// Update is called once per frame
	void Update () {
		if (canshoot)
			StartCoroutine (Shoot ());
	}

	IEnumerator Shoot (){
		canshoot = false;
		anim.SetBool ("firing", true);
		Rigidbody fb0 = Instantiate (fireball, fireball_spawnpoint.position, fireball_spawnpoint.rotation);
		Rigidbody fb1 = Instantiate (fireball, fireball_spawnpoint.position, fireball_spawnpoint.rotation);
		Rigidbody fb2 = Instantiate (fireball, fireball_spawnpoint.position, fireball_spawnpoint.rotation);
		yield return new WaitForSeconds (power_up_time);
		fb0.velocity = shootdirs [0] * fireball_speed;
		fb1.velocity = shootdirs [1] * fireball_speed;
		fb2.velocity = shootdirs [2] * fireball_speed;
		Debug.Log ("Aquamentus: pew");
		anim.SetBool ("firing", false);
		yield return new WaitForSeconds (Random.Range (min_reload_time, max_reload_time));
		canshoot = true;
	}

	IEnumerator Move(){
		yield return null;
	}
}
