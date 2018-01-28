using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderMovement : MonoBehaviour {
	public float max_range = 20f;
	public float attack_speed = 3f;
	public float recover_speed = 1f;
	public Rigidbody[] partners;

	public bool can_attack = true;
	bool hit_partner = false;
	int layer_mask = (1 << 9);
	Vector3 init_pos;
	Rigidbody rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		init_pos = rb.position;
	}
	
	// Update is called once per frame
	void Update () {
		foreach (Rigidbody r in partners) {
			Debug.DrawLine (init_pos, r.position);
			if (Physics.Raycast (init_pos, r.position - transform.position, max_range, layer_mask)) {
				if (r.GetComponent<SliderMovement> ().can_attack == false)
					continue;
				StartCoroutine (Activate (r));
				StartCoroutine (r.GetComponent<SliderMovement> ().Activate (rb));
			}
		}

		if (!can_attack)
			return;
	}

	void OnCollisionEnter(Collision coll){
		Debug.Log ("Collision!!!");
		if (coll.collider.gameObject.GetComponent<SliderMovement> () != null) {
			Debug.Log ("hit partner!!!");
			hit_partner = true;
		}
	}

	public IEnumerator Activate(Rigidbody r){
		can_attack = false;

		while (hit_partner == false) {
			rb.position += (r.position - transform.position).normalized * attack_speed * Time.deltaTime;
			yield return null;
		}

		while (Mathf.Abs (Vector3.Magnitude (transform.position - init_pos)) > 0.1) {
			rb.position += (init_pos - transform.position).normalized * recover_speed * Time.deltaTime;
			yield return null;
		}

		rb.position = init_pos;
		rb.velocity = Vector3.zero;
		hit_partner = false;
		can_attack = true;
		yield return null;
	}
}
