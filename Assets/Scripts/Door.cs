using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
	public bool locked = false;

	public GameObject main_camera;
	public GameObject[] locked_door;
	public GameObject[] unlocked_door;

	public void Unlock ()
	{
		if (locked == false)
			return;

		Debug.Log ("Unlocking door");

		locked = false;
		foreach (GameObject obj in locked_door){
			obj.SetActive (false);
		}
		foreach (GameObject obj in unlocked_door) {
			obj.SetActive (true);
		}
	}
}
