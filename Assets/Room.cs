using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

	GameObject[] active_members;

	// Use this for initialization
	void Start () {
		foreach (GameObject obj in active_members)
			obj.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
