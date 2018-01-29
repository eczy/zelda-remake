using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBlocks : MonoBehaviour {

	public Transform[] blocks;
	public float delay = 2f;

	Vector3[] original_pos;

	// Use this for initialization
	void Start () {
		original_pos = new Vector3[blocks.Length];
		for (int i = 0; i < blocks.Length; i++) {
			original_pos [i] = blocks [i].position;
		}
	}

	IEnumerator Reset (){
		yield return new WaitForSeconds (delay);
		for (int i = 0; i < blocks.Length; i++) {
			blocks [i].position = original_pos [i];
			blocks [i].GetComponent<pushable_wall> ().Reset ();
		}
	}

	void OnTriggerEnter(Collider coll){
		if (coll.GetComponent<ArrowKeyMovement> () != null) {
			StartCoroutine (Reset ());
		}
	}
}
