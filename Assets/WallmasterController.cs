using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallmasterController : MonoBehaviour {

	public Transform[] spawn_points;
	public Rigidbody wallmaster;
	public int max_num_wallmasters = 1;
	public int num_wallmasters = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (num_wallmasters < max_num_wallmasters) {
			SpawnNewWallmaster ();
		}
	}

	void SpawnNewWallmaster(){
		int r = Random.Range (0, spawn_points.Length);
		Instantiate (wallmaster, spawn_points [r].position, spawn_points [r].rotation);
		num_wallmasters++;
	}
}
