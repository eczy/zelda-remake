using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnOnFireballHit : MonoBehaviour {

	public Transform flame;
	public float burn_time = 1f;

	bool burning = false;
	Coroutine co;

	void OnTriggerEnter(Collider coll){
		if (burning && coll.GetComponent<friendly_Fireball> () != null) {
			StopCoroutine (co);
			co = StartCoroutine (Burn ());
			return;
		}

		if (coll.GetComponent<friendly_Fireball> () != null) {
			co = StartCoroutine (Burn ());
		}
	}

	IEnumerator Burn(){
		burning = true;
		Transform inst_flame;
		inst_flame = Instantiate (flame, transform.position, transform.rotation);
		inst_flame.parent = this.transform;
		inst_flame.GetComponent<SpriteRenderer> ().sortingOrder = 1;

		yield return new WaitForSeconds (burn_time);
		Destroy (inst_flame.gameObject);
		burning = false;
	}
}
