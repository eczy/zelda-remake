using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputToAnimator : MonoBehaviour {

	Animator animator;
	ArrowKeyMovement controller;
	Health health;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		controller = GetComponent<ArrowKeyMovement> ();
		health = GetComponent<Health> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (controller.GetControlsDisabled () == true) {
			animator.speed = 0.0f;
			return;
		}
		
		float horizontal_input = Input.GetAxisRaw ("Horizontal");
		float vertical_input = Input.GetAxisRaw ("Vertical");
		
		// Prevent confusing the animatior
		if (Mathf.Abs (horizontal_input) > 0.0f)
			vertical_input = 0.0f;
		
		animator.SetFloat ("horizontal_input", horizontal_input);
		animator.SetFloat ("vertical_input", vertical_input);

		if (Input.GetAxisRaw ("Horizontal") == 0 && Input.GetAxisRaw ("Vertical") == 0)
		{
			animator.speed = 0.0f;
		}
		else
		{
			animator.speed = 1.0f;
		}
	}
}
