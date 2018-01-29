using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearingWall : MonoBehaviour {

	public Transform cloud;
	public Transform[] blocks;
	public float rate_of_appearance = 0.3f;
	public float cloud_time = 0.1f;

	void Start(){
		foreach (Transform block in blocks) {
			block.gameObject.SetActive (false);
		}
	}

	public IEnumerator Appear(){
		for (int i = 0; i < blocks.Length; i++) {
			StartCoroutine (ShowBlock (i));
			yield return new WaitForSeconds (rate_of_appearance);
		}
	}

	IEnumerator ShowBlock(int i){
		Transform c = Instantiate (cloud, blocks [i].position, blocks [i].rotation);
		yield return new WaitForSeconds (cloud_time);
		Destroy (c.gameObject);
		blocks [i].gameObject.SetActive (true);
	}
}
