using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aquamentus_friendly : EnemyController {

	public float max_reload_time = 3f;
	public float min_reload_time = 0.5f;
	public float power_up_time = 0.5f;
	public float delta_x = 0.1f;
	public float change_dir_delay = 0.5f;
	public Transform fireball_spawnpoint;
	public Rigidbody fireball;
	public float fireball_speed = 2f;
	public float shot_spread = 0.25f;
	public Transform enemy;

	bool canshoot = true;
	bool forward = true;
	Animator anim;
	Rigidbody rb;
	Enemy status;
	float startposx;

	Vector3[] shootdirs = {
		new Vector3 (-3, 1, 0).normalized,
		new Vector3 (-3, -1, 0).normalized,
		new Vector3 (-1, 0, 0)
	};

	Rigidbody[] fireballs = new Rigidbody[3];

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody> ();
		startposx = transform.position.x;


		status = GetComponent<Enemy> ();
		//StartCoroutine (Move ());
	}
	
	// Update is called once per frame
	void Update () {
		if (canshoot)
			StartCoroutine (Shoot ());

        Debug.DrawRay (fireball_spawnpoint.position, fireball_spawnpoint.forward);

        fireball_spawnpoint.forward = GetComponent<Transform>().forward - fireball_spawnpoint.transform.position;

		shootdirs [0] = Vector3.RotateTowards(fireball_spawnpoint.forward, fireball_spawnpoint.up, shot_spread, 0f);
		shootdirs [1] = Vector3.RotateTowards(fireball_spawnpoint.forward, -fireball_spawnpoint.up, shot_spread, 0f);
		shootdirs [2] = fireball_spawnpoint.forward;
	}


	IEnumerator Shoot (){
		canshoot = false;
		anim.SetBool ("firing", true);

		fireballs [0] = Instantiate (fireball, fireball_spawnpoint.position, transform.rotation);
		fireballs [1] = Instantiate (fireball, fireball_spawnpoint.position, transform.rotation);
		fireballs [2] = Instantiate (fireball, fireball_spawnpoint.position, transform.rotation);

		yield return new WaitForSeconds (power_up_time);

		for (int i = 0; i < fireballs.Length; ++i){
			if (fireballs [i] != null)
				fireballs [i].velocity = shootdirs [i] * fireball_speed;
		}
		Debug.Log ("Aquamentus: pew");
		anim.SetBool ("firing", false);
		yield return new WaitForSeconds (Random.Range (min_reload_time, max_reload_time));
		canshoot = true;
	}

	IEnumerator Move(){
		yield return new WaitForSeconds (change_dir_delay);
		int r = Random.Range (0, 2);
		if (r == 0)
			forward = true;
		else
			forward = false;

		StartCoroutine (Move ());
	}
}
