using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpFromBowRoom : MonoBehaviour {

	public GameObject player_warp_point;
	public GameObject camera_warp_point;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider coll){
		if (coll.GetComponent<Health> () != null) {
			coll.transform.position = player_warp_point.transform.position;
			Camera.main.transform.position = camera_warp_point.transform.position;
		}
	}
}
