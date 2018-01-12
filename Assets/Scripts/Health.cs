using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	float health = 3.0f;
	public int max_health = 3;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public float GetHealth()
	{
		return health;
	}

	public void SetHealth(float num_health)
	{
		health = num_health;
	}

	public void Damage(float damage)
	{
		health -= damage;
	}

	public void Heal(float num_heal)
	{
		health += num_heal;
		if (health > max_health)
			health = max_health;
	}
}
