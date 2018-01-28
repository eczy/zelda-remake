using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderMovement : MonoBehaviour {
	public float max_dist_x = 1f;
	public float max_dist_y = 1f;
	public float attack_speed = 3f;
	public float recover_speed = 1f;
	public bool up = false;
	public bool down = false;
	public bool left = false;
	public bool right = false;

	bool can_attack = true;
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
		Debug.DrawLine (init_pos, init_pos + new Vector3 (0, max_dist_y, 0) * 1f);
		Debug.DrawLine (init_pos, init_pos + new Vector3 (0, -max_dist_y, 0) * 1f);
		Debug.DrawLine (init_pos, init_pos + new Vector3 (max_dist_x, 0, 0) * 1f);
		Debug.DrawLine (init_pos, init_pos + new Vector3 (-max_dist_x, 0, 0) * 1f);

		if (!can_attack)
			return;
		
		if (Physics.Raycast (init_pos, Vector3.up, max_dist_y, layer_mask) && up)
			StartCoroutine (Activate (Vector3.up));
		else if (Physics.Raycast (init_pos, -Vector3.up, max_dist_y, layer_mask) && down)
			StartCoroutine (Activate (-Vector3.up));
		else if (Physics.Raycast (init_pos, Vector3.right, max_dist_x, layer_mask) && right)
			StartCoroutine (Activate (Vector3.right));
		else if (Physics.Raycast (init_pos, -Vector3.right, max_dist_x, layer_mask) && left)
			StartCoroutine (Activate (-Vector3.right));
	}

	IEnumerator Activate(Vector3 direction){
		can_attack = false;
		if (Mathf.Abs(direction.x) > 0) {
			while (Mathf.Abs (init_pos.x - rb.position.x) < max_dist_x-0.5) {
				rb.position += direction * attack_speed * Time.deltaTime;
				yield return null;
			}
			while (Mathf.Abs(init_pos.x - rb.position.x) > 0.1) {
				rb.position += -direction * recover_speed * Time.deltaTime;
				yield return null;
			}
		} else {
			while (Mathf.Abs (init_pos.y - rb.position.y) < max_dist_y-0.5) {
				rb.position += direction * attack_speed * Time.deltaTime;
				yield return null;
			}
			while (Mathf.Abs(init_pos.y - rb.position.y) > 0.1) {
				rb.position += -direction * recover_speed * Time.deltaTime;
				yield return null;
			}
		}

		rb.position = init_pos;
		can_attack = true;
		yield return null;
	}
}
