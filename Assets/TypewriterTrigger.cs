using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypewriterTrigger : MonoBehaviour {

	public Typewriter t;

	void OnTriggerEnter(Collider coll){
		if (coll.GetComponent<ArrowKeyMovement> () != null) {
			StartCoroutine (t.Go ());
			Destroy (gameObject.GetComponent<Collider> ());
		}
	}
}
