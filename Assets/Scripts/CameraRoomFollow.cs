using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRoomFollow : MonoBehaviour {

	public float transition_time;
	public Vector3 cam_delta = new Vector3 (16, 11, 0);
	public Vector3 player_delta;

	ArrowKeyMovement controller;
	Transform player_transform;
	GameObject main_camera;
	BoxCollider player_bc;

	swingSword sword;
	BombUse bomb;
	UseBoomerang boom;
	Bow bow;


	void Start ()
	{
		controller = GetComponent<ArrowKeyMovement> ();
		player_transform = GetComponent<Transform> ();
		main_camera = GameObject.FindGameObjectWithTag ("MainCamera");
		player_bc = GetComponent<BoxCollider> ();

		sword = GetComponent<swingSword> ();
		bomb = GetComponent<BombUse> ();
		boom = GetComponent<UseBoomerang> ();
		bow = GetComponent<Bow> ();
	}

	void OnTriggerEnter (Collider coll)
	{
		GameObject object_collided_with = coll.gameObject;
		CameraTransitionTrigger cam_trigger = object_collided_with.GetComponent<CameraTransitionTrigger> ();
		if (cam_trigger != null) {
			Vector3 cam_position = main_camera.transform.position;
			Vector3 target = cam_position + Vector3.Scale (cam_delta, cam_trigger.transition_direction);
			Debug.Log ("Moving camera to " + target.ToString ());
			StartCoroutine (Transition (main_camera.transform, target, transition_time, Vector3.Scale (player_delta, cam_trigger.transition_direction)));
		}
	}

	IEnumerator Transition(Transform camera_transform, Vector3 end_position, float duration, Vector3 p_delta)
	{
		controller.DisableControls ();
		if (sword != null)
			sword.enabled = false;
		if (bow != null)
			bow.enabled = false;
		if (bomb != null)
			bomb.enabled = false;
		if (boom != null)
			boom.enabled = false;
		player_bc.enabled = false;
		foreach (Transform child in transform) {
			if (child.GetComponent<Collider> () != null)
				child.GetComponent<Collider> ().enabled = false;
		}
		Vector3 cam_start_position = camera_transform.position;
		Vector3 player_start_position = player_transform.position;
		Vector3 player_end_position = player_start_position + p_delta;
		float t = 0.0f;
		while(t < 1)
		{
			t += Time.deltaTime / duration;
			camera_transform.position = Vector3.Lerp (cam_start_position, end_position, t);
			player_transform.position = Vector3.Lerp (player_start_position, player_end_position, t);
			yield return null;
		}
		transform.position = player_end_position;
		player_bc.enabled = true;
		if (sword != null)
			sword.enabled = true;
		if (bow != null)
			bow.enabled = true;
		if (bomb != null)
			bomb.enabled = true;
		if (boom != null)
			boom.enabled = true;
		foreach (Transform child in transform) {
			if (child.GetComponent<Collider> () != null)
				child.GetComponent<Collider> ().enabled = true;
		}
		controller.EnableControls ();
	}
}
