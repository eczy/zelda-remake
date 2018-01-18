using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	public float damage = 1.0f;
	public float health = 3.0f;
	public float flash_duration = 0.5f;
	public float sword_damage = 1.0f;
	public float arrow_damage = 1.0f;
	public float bomb_damage = 1.0f;
	public float knockback_force;
	public float movement_delay_on_hit = 0.5f;

	FlashWhenDamaged flash;
	Rigidbody rb;

	void Start ()
	{
		flash = GetComponent<FlashWhenDamaged> ();
		rb = GetComponent<Rigidbody> ();
	}

	void Update()
	{
		if (health == 0)
			Destroy (gameObject);
	}

	public void Damage(float amount)
	{
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
		} else if (other.GetComponent<Sword> () != null) {
			Debug.Log ("Enemy hit by sword");
			Damage (sword_damage);

			// Destroy if it is a magic sword beam
			if (other.GetComponent<Rigidbody> () != null)
				Destroy (other);
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
		rb.AddForce (direction * knockback_force, ForceMode.Impulse);
		yield return  new WaitForSeconds (movement_delay_on_hit);
	}
}
