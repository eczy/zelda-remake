﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	public float damage = 1.0f;
	public float health = 3.0f;
	public float flash_duration = 0.5f;
	public float sword_damage = 1.0f;
	public float arrow_damage = 1.0f;
	public float bomb_damage = 1.0f;
	public float boom_damage = 1.0f;
    public float fireball_damage = 1.0f;
	public float knockback_force;
	public float movement_delay_on_hit = 0.5f;
	public AudioClip hurt_sound = null;
	public AudioClip die_sound = null;
	public EnemyController controller;
	public Transform carried_object = null;
	public Transform heart_drop;
	public Transform bomb_drop;
	public Transform rupee_drop;
	public float drop_prob = 0.50f;
	public float heart_drop_prob = 0.35f;
	public float bomb_drop_prob = 0.30f;
	public float rupee_drop_prob = 0.35f;
	public bool isProjectile = false;
	FlashWhenDamaged flash;
	Rigidbody rb;

	void Start ()
	{
		flash = GetComponent<FlashWhenDamaged> ();
		rb = GetComponent<Rigidbody> ();
		if (carried_object != null)
			carried_object.GetComponent<Collider> ().enabled = false;
	}

	void Update()
	{
		if (health <= 0) {
			if (die_sound)
				AudioSource.PlayClipAtPoint (die_sound, Camera.main.transform.position);
			if (carried_object != null) {
				carried_object.GetComponent<Collider> ().enabled = true;
				carried_object.parent = null;
			}

			float r = Random.value;
			if (r < drop_prob) {
				r = Random.value;
				float heart_lim = heart_drop_prob;
				float bomb_drop_lim = heart_lim + bomb_drop_prob;
				float rupee_drop_lim = 1f;
				if (r <= heart_lim) {
					Instantiate (heart_drop, transform.position, transform.rotation);
				} else if (r > heart_lim && r <= bomb_drop_lim) {
					Instantiate (bomb_drop, transform.position, transform.rotation);
				} else {
					Instantiate (rupee_drop, transform.position, transform.rotation);
				}
			}
			Destroy (gameObject);
		}
	}

	public void Damage(float amount)
	{
		if(hurt_sound)
			AudioSource.PlayClipAtPoint (hurt_sound, Camera.main.transform.position);
		health -= amount;
		if (flash != null)
			flash.Flash (flash_duration);
	}



	void OnTriggerEnter(Collider coll)
	{
		GameObject other = coll.gameObject;
		if (other.GetComponent<Arrow> () != null) {
			Debug.Log ("Enemy hit by arrow");
			Damage (arrow_damage);
			Destroy (other);
		}
        else if (other.GetComponent<Sword> () != null) {
			Debug.Log ("Enemy hit by sword");
			Damage (sword_damage);

			// Destroy if it is a magic sword beam
			if (other.GetComponent<Rigidbody> () != null && isProjectile == false)
				Destroy (other);
		} else if (other.GetComponent<Bomb> () != null) {
			Debug.Log ("Enemy hit by bomb");
			Damage (bomb_damage);
		}
        else if (other.GetComponent<Boomerang>() != null)
        {
            Debug.Log("Enemy hit by boomerang");
			Damage (boom_damage);
            if (gameObject.tag == "stalfo")
            {
                return;
            }
        }
        else if(other.GetComponent<friendly_Fireball>() != null)
        {
            Debug.Log("Enemy hit by fireball");
            Damage(fireball_damage);
        }

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
		StartCoroutine (Knockback (dir, movement_delay_on_hit));
	}

	IEnumerator Knockback(Vector3 direction, float movement_delay_on_hit)
	{
		if (controller != null)
			controller.enabled = false;
		rb.AddForce (direction * knockback_force, ForceMode.Impulse);
		yield return new WaitForSeconds (movement_delay_on_hit);
		if (controller != null)
			controller.enabled = true;
		yield return null;
	}
}
