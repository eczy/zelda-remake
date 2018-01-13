using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputToAnimator : MonoBehaviour {

    Animator animator;

	enum Direction{North, East, South, West};

	Direction linkDirection;

	// Use this for initialization
	void Start () {

        animator = GetComponent<Animator>();

		linkDirection = Direction.South;
	}
	
	// Update is called once per frame
	void Update () {
        animator.SetFloat("horizontal_input", Input.GetAxisRaw("Horizontal"));
        animator.SetFloat("vertical_input", Input.GetAxisRaw("Vertical"));
		//down arrow is pressed
		if (animator.GetFloat("vertical_input") == -1f) {
			animator.SetBool ("running_down", true);
			linkDirection = Direction.South;
			Debug.Log (linkDirection.ToString ());
		} else {
			animator.SetBool ("running_down", false);
		}
		if(animator.GetFloat("vertical_input") == 1f){
			linkDirection = Direction.North;
			Debug.Log (linkDirection.ToString ());
		}
		if (animator.GetFloat ("horizontal_input") == -1f) {
			linkDirection = Direction.West;
			Debug.Log (linkDirection.ToString ());
		}
		if (animator.GetFloat ("horizontal_input") == 1f) {
			linkDirection = Direction.East;
			Debug.Log (linkDirection.ToString ());
		}
		//Debug.Log("running_down is "+ animator.GetBool("running_down"));
        if(Input.GetAxisRaw("Horizontal")==0 && Input.GetAxisRaw("Vertical") == 0)
        {
            animator.speed = 0.0f;
        }
        else
        {
            animator.speed = 1.0f;
        }
    }
}
