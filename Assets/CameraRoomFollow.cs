using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRoomFollow : MonoBehaviour {

	void OnTriggerEnter (Collider coll)
	{
		GameObject object_collided_with = coll.gameObject;
		Door door = object_collided_with.GetComponent<Door> ();


	}
}
