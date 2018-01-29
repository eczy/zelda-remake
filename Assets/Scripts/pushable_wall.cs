using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushable_wall : MonoBehaviour {

	public float movement_time = 1f;
	public AudioClip jingle = null;
	public float jingle_delay;
	public Door secret_door = null;
	public AppearingWall[] walls;
	public SliderMovement[] traps;

	bool moving = false;
	Rigidbody rb;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
		rb.isKinematic = true;
    }

    private void OnCollisionStay(Collision coll)
    {
		if (moving)
			return;
		
		if (coll.collider.tag=="Player")
        {
			Vector3 direction;
			Vector3 diff = transform.position - coll.collider.transform.position;
			if (Mathf.Abs (diff.x) > Mathf.Abs (diff.y))
				direction = new Vector3 (1, 0, 0) * Mathf.Sign (diff.x);
			else
				direction = new Vector3 (0, 1, 0) * Mathf.Sign (diff.y);

			if (Physics.Raycast (transform.position, direction, 1f, (1 << 14 ) | ( 1 << 0)))
				return;
			
			StartCoroutine (Move (direction));
        }
    }

	IEnumerator Move(Vector3 dir){
		rb.isKinematic = false;
		moving = true;
		float t = 0;
		Vector3 start = rb.position;
		Vector3 end = rb.position + dir;
		Debug.Log ("moving to " + end.ToString ());
		foreach (SliderMovement s in traps){
			s.enabled = false;
		}
		foreach (AppearingWall wall in walls) {
			StartCoroutine(wall.Appear ());
		}
		while (t < movement_time) {
			t += Time.deltaTime;
			float progress = t / movement_time;
			rb.position = Vector3.Lerp (start, end, progress);
			yield return null;
		}
		rb.position = end;
		rb.isKinematic = true;
		if (secret_door != null)
			secret_door.Unlock ();
		if (jingle != null) {
			yield return new WaitForSeconds (jingle_delay);
			AudioSource.PlayClipAtPoint (jingle, Camera.main.transform.position);
		}
	}

	public void Reset(){
		moving = false;
	}
}