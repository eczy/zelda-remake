using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour {

	public float arrow_speed;
	public float spawn_distance;
	public Rigidbody arrow_up;
	public Rigidbody arrow_down;
	public Rigidbody arrow_left;
	public Rigidbody arrow_right;
	public float reload_time = 1.0f;
	public float control_delay = 0.25f;

	ArrowKeyMovement controller;
	Inventory inventory;
	bool reloading = false;

	// Use this for initialization
	void Start () {
		controller = GetComponent<ArrowKeyMovement> ();
		inventory = GetComponent<Inventory> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Z) && !reloading) {
			StartCoroutine(Shoot ());
		}
	}

	IEnumerator Shoot ()
	{
		int rupees = inventory.GetRupees ();
		if (rupees == 0) {
			Debug.Log ("Cannot shoot arrows with no rupees");
			yield break;
		}

		controller.DisableControls ();
		reloading = true;

		inventory.SetRupees (rupees - 1);
		Vector3 forward = controller.Forward ();
		Transform transform = GetComponent<Transform> ();

		Rigidbody arrow;
		if (forward.x > 0)
			arrow = arrow_right;
		else if (forward.x < 0)
			arrow = arrow_left;
		else if (forward.y > 0)
			arrow = arrow_up;
		else if (forward.y < 0)
			arrow = arrow_down;
		else
			arrow = arrow_up;
		
		Rigidbody spawned_arrow = Instantiate (arrow, transform.position + forward * spawn_distance, transform.rotation);
		spawned_arrow.velocity = forward * arrow_speed;

		StartCoroutine(RestoreControls ());
		yield return new WaitForSeconds(reload_time);
		reloading = false;
	}

	IEnumerator RestoreControls (){
		yield return new WaitForSeconds (control_delay);
		controller.EnableControls ();
	}
}
