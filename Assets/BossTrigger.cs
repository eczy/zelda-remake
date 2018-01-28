using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour {

	public Aquamentus boss;
	public float activation_delay = 0.5f;

	void OnTriggerEnter(Collider coll){
		if (coll.GetComponent<ArrowKeyMovement> ()) {
			StartCoroutine (Trigger ());
		}
	}

	IEnumerator Trigger(){
		yield return new WaitForSeconds (activation_delay);
		boss.player_present = !boss.player_present;
	}
}
