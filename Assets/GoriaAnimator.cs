using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoriaAnimator : MonoBehaviour {
	Animator animator;
	GoriaMovement controller;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		controller = GetComponent<GoriaMovement> ();
	}

	// Update is called once per frame
	void Update () {

		Vector3 direction = controller.current_dir;
		animator.SetFloat ("horizontal", direction.x);
		animator.SetFloat("vertical", direction.y);
	}
}
