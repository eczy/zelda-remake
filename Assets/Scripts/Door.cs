﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
	public bool locked = false;
	public bool isUnlockable = true;

	public GameObject main_camera;
	public GameObject[] locked_door;
	public GameObject[] unlocked_door;
	public AudioClip unlock_sound;
	public Door other_side = null;

	public void Unlock ()
	{
		if (locked == false)
			return;

		AudioSource.PlayClipAtPoint (unlock_sound, Camera.main.transform.position);
		Debug.Log ("Unlocking door");

		locked = false;
		foreach (GameObject obj in locked_door){
			obj.SetActive (false);
		}
		foreach (GameObject obj in unlocked_door) {
			obj.SetActive (true);
		}

		if (other_side != null)
			other_side.Unlock ();
	}

	public void Lock()
	{
		if (locked == true)
			return;

		AudioSource.PlayClipAtPoint (unlock_sound, Camera.main.transform.position);
		Debug.Log ("Locking door");

		locked = true;
		foreach (GameObject obj in locked_door){
			obj.SetActive (true);
		}
		foreach (GameObject obj in unlocked_door) {
			obj.SetActive (false);
		}
	}
}
