using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayDestroy : MonoBehaviour {

	void OnTriggerEnter(){
		StartCoroutine (Boom ());
	}
	IEnumerator Boom () {
		yield return null;
		Destroy (GetComponent<Collider> ());
	}
}
