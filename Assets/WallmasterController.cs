using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallmasterController : MonoBehaviour {

	public Transform spawn_point;
	public Transform[] movement_points;
	public WallmasterMovement wallmaster;
	public float spawn_delay = 2f;
	bool canspawn = true;
	int max_wallmasters = 2;
	int num_wallmasters = 0;

	void OnTriggerStay(Collider coll){
		if (num_wallmasters >= max_wallmasters || !canspawn)
			return;
		
		if (coll.GetComponent<ArrowKeyMovement> () == null)
			return;
		
		WallmasterMovement wm = Instantiate (wallmaster, spawn_point.position, spawn_point.rotation);
		wm.move_points = movement_points;
		wm.controller = this;
		++num_wallmasters;
		StartCoroutine (Cooldown ());
	}

	IEnumerator Cooldown(){
		canspawn = false;
		yield return new WaitForSeconds (spawn_delay);
		canspawn = true;
	}

	public void WallmasterDied(){
		num_wallmasters--;
	}
}
