using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour {

	public float arrow_speed=10f;
	public float spawn_distance;
	public Rigidbody arrow_up;
	public Rigidbody arrow_down;
	public Rigidbody arrow_left;
	public Rigidbody arrow_right;
	public Sprite shoot_up_sprite;
	public Sprite shoot_down_sprite;
	public Sprite shoot_left_sprite;
	public Sprite shoot_right_sprite;
	public float reload_time = 1.0f;
	public float control_delay = 0.25f;

	ArrowKeyMovement controller;
	Inventory inventory;
	SpriteRenderer renderer;
	Animator animator;
	bool reloading = false;

	// Use this for initialization
	void Start () {
		controller = GetComponent<ArrowKeyMovement> ();
		inventory = GetComponent<Inventory> ();
		renderer = GetComponent<SpriteRenderer> ();
		animator = GetComponent<Animator> ();
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
		Sprite original_sprite = renderer.sprite;
		animator.enabled = false;
		if (forward.x > 0) {
			arrow = arrow_right;
			renderer.sprite = shoot_right_sprite;
		} else if (forward.x < 0) {
			arrow = arrow_left;
			renderer.sprite = shoot_left_sprite;
		} else if (forward.y > 0) {
			arrow = arrow_up;
			renderer.sprite = shoot_up_sprite;
		} else if (forward.y < 0) {
			arrow = arrow_down;
			renderer.sprite = shoot_down_sprite;
		} else {
			arrow = arrow_up;
		}
		
		Rigidbody spawned_arrow = Instantiate (arrow, transform.position + forward * spawn_distance, transform.rotation);
		spawned_arrow.velocity = forward * arrow_speed;

		StartCoroutine(Reload ());
		yield return new WaitForSeconds(control_delay);
		renderer.sprite = original_sprite;
		animator.enabled = true;
		controller.EnableControls ();
	}

	IEnumerator Reload (){
		yield return new WaitForSeconds (reload_time);
		reloading = false;
	}
}
