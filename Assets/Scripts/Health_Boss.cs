﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health_Boss : MonoBehaviour {

	public float health = 3.0f;
	public float max_health = 3f;
	public float recovery_time = 2.0f;
	public float control_disable_time = 0.5f;
	public float knockback_force = 10.0f;
	public GameObject death_panel;
	public AudioClip hurt_sound;
	public AudioClip die_sound;
	public AudioClip low_health_sound;
	public Text healthtext;

    GameObject link_with_boss;
    GameObject idle_aquamentus;
    GameObject player;

    ArrowKeyMovement controller;
	Rigidbody rb;
	FlashWhenDamaged flash;
//	AudioSource audio;
	Coroutine co_recover;
	Coroutine co_pause_controls;
	bool recovering = false;

    void Awake()
    {
        idle_aquamentus = GameObject.Find("mountable_aquamentus");
        player = GameObject.Find("Player");
        link_with_boss = GameObject.Find("link_ride_boss");

    }

    void Start()
	{
		controller = GetComponent<ArrowKeyMovement> ();
		rb = GetComponent<Rigidbody> ();
		flash = GetComponent<FlashWhenDamaged> ();
//		audio = GetComponent<AudioSource> ();
//		audio.enabled = false;
	}

	void Update()
	{
//		if (health <= 1)
//			audio.enabled = true;
//		else
//			audio.enabled = false;
		healthtext.text = "Aquamentus Health: " + health + "/" + max_health;
	}

	public bool FullHealthFlag(){
		return max_health == health;
	}

	void OnCollisionEnter (Collision coll)
	{
		GameObject object_collided_with = coll.collider.gameObject;
		Enemy enemyobject = object_collided_with.GetComponent<Enemy> ();

		if (enemyobject == null || recovering)
			return;

		Damage (enemyobject.damage);

		if (health <= 0)
			return;
		
		co_recover = StartCoroutine (Recover());
		co_pause_controls = StartCoroutine (PauseControls());
		StartCoroutine(Knockback(coll.contacts [0].normal));
	}

	void OnCollisionStay(Collision coll){
		OnCollisionEnter (coll);
	}

	public float GetHealth()
	{
		return health;
	}

	public void SetHealth(float num_health)
	{
		health = num_health;
		if (health > max_health)
			health = max_health;
	}

	void Damage(float damage)
	{
		AudioSource.PlayClipAtPoint (hurt_sound, Camera.main.transform.position);
		health -= damage;
		if (health <= 0) {
			health = 0;
			Die ();
		}
	}
		
	public void Heal(float num_heal)
	{
		health += num_heal;
		if (health > max_health)
			health = max_health;
	}

	IEnumerator Knockback(Vector3 direction)
	{
		rb.AddForce (direction * knockback_force, ForceMode.Impulse);
		yield return null;
	}

	IEnumerator Recover()
	{
		recovering = true;
		if (flash != null)
			flash.Flash (recovery_time);
		yield return new WaitForSeconds (recovery_time);
		recovering = false;
	}

	IEnumerator PauseControls()
	{
		controller.DisableControls ();
		yield return new WaitForSeconds (control_disable_time);
		controller.EnableControls ();
	}
		
	void Die()
	{
		Debug.Log ("Dragon died!");
		AudioSource.PlayClipAtPoint (die_sound, Camera.main.transform.position);
		StopCoroutine (co_recover);
		StopCoroutine (co_pause_controls);
		controller.DisableControls ();

		death_panel.SetActive (true);
		gameObject.SetActive (false);
		return;
        
		/*
        Vector3 link_position = link_with_boss.GetComponent<Transform>().position;
        player.GetComponent<Transform>().position = link_position;
        link_with_boss.SetActive(false);
        player.SetActive(true);
        */

		Vector3 link_with_boss_position = link_with_boss.GetComponent<Transform> ().position;
		Vector3 link_position = link_with_boss_position + new Vector3 (-1.5f, 0, 0);
		Vector3 dragonScale = link_with_boss.GetComponent<Transform> ().localScale;
		//dragonScale.x *= -1;
		idle_aquamentus.GetComponent<Transform> ().position = link_with_boss_position;
		idle_aquamentus.GetComponent<Transform> ().localScale = dragonScale;
		player.GetComponent<Transform> ().position = link_position;
		player.SetActive (true);
		link_with_boss.SetActive (false);
	}
}
