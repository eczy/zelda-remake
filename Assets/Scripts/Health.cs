using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

	public float health = 3.0f;
	public int max_health = 3;
	public float recovery_time = 2.0f;
	public float control_disable_time = 0.5f;
	public float knockback_force = 10.0f;
	public GameObject death_panel;

	ArrowKeyMovement controller;
	Rigidbody rb;
	FlashWhenDamaged flash;
	Coroutine co_recover;
	Coroutine co_pause_controls;
	bool recovering = false;

	void Start()
	{
		controller = GetComponent<ArrowKeyMovement> ();
		rb = GetComponent<Rigidbody> ();
		flash = GetComponent<FlashWhenDamaged> ();
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
		Knockback(coll.contacts [0].normal);
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

	void Knockback(Vector3 direction)
	{
		rb.AddForce (direction * knockback_force, ForceMode.Impulse);
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
		Debug.Log ("You died!");
		StopCoroutine (co_recover);
		StopCoroutine (co_pause_controls);
		controller.DisableControls ();
		death_panel.SetActive (true);
	}
}
