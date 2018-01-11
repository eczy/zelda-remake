using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowKeyMovement : MonoBehaviour {

	// Inspector fields
	public float movement_speed = 4;

	Rigidbody rb;

	// Use this for initialization
	void Start ()
	{
		// Store a reference to the Rigidbody component on this game object.
		// This prevents us from calling GetComponent() every frame, which saves performance
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector2 current_input = GetInput ();
		rb.velocity = current_input * movement_speed;
	}
	 
	Vector2 GetInput ()
	{
		// The following Input.GetAxis calls make use of the Unity InputManager.
		// The InputManager allows us to standardize controls without hard-coding specific keys or buttons
		// For instance, the code below would work with a keyboard, OR a controller, without changes!
		// Read up here: https://docs.unity3d.com/Manual/class-InputManager.html

		// Grab the current value of the right-left arrow keys.
		float horizontal_input = Input.GetAxisRaw ("Horizontal");
		// Grab the current value of the up-down arrow keys.
		float vertical_input = Input.GetAxisRaw ("Vertical");

		// Don't allow diagonal movement.
		if (Mathf.Abs (horizontal_input) > 0.0f)
			vertical_input = 0.0f;

		return new Vector2 (horizontal_input, vertical_input);
	}
}
