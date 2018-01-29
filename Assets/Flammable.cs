using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flammable : MonoBehaviour {

	public Transform[] flammable_objects;
	public Transform flame;
	public float burn_time;

	bool burning = false;
	Transform[] flames;

	void Start(){
		flames = new Transform[flammable_objects.Length];
	}

	void OnTriggerEnter(Collider coll){
		if (burning)
			return;
		
		if (coll.GetComponent<Fireball> () != null || coll.GetComponent<friendly_Fireball> () != null) {
			StartCoroutine (Burn ());
		}
	}

	IEnumerator Burn(){
		burning = true;
		for (int i = 0; i < flammable_objects.Length; i++) {
			flames [i] = Instantiate (flame, flammable_objects [i].position, flammable_objects [i].rotation);
			flames [i].GetComponent<SpriteRenderer> ().sortingOrder = 2;
		}

		yield return new WaitForSeconds (burn_time);

		for (int i = 0; i < flammable_objects.Length; i++) {
			Destroy (flammable_objects [i].gameObject);
			Destroy (flames [i].gameObject);
		}
	}
}
