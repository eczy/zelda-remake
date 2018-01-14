using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransitionTrigger : MonoBehaviour {
	public Vector3 transition_direction;

	void Start()
	{
		transition_direction.Normalize ();
	}
}
