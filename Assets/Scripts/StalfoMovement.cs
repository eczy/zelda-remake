using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalfoMovement : EnemyController {

	Rigidbody rb;
	public float move_speed = 0.5f;
	public float change_dir_time = 1f;
    public bool isStunned = false;

	Vector3 current_dir;
	int layer_mask = ~((1 << 8) | (1 << 9));
	Coroutine co;

	Vector3[] directions = {
		new Vector3 (1, 0, 0),
		new Vector3 (0, 1, 0),
		new Vector3 (-1, 0, 0),
		new Vector3 (0, -1, 0)
	};

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		co = StartCoroutine (Move ());
	}

	void Update (){
		Debug.DrawLine (transform.position, transform.position + current_dir * 1f);
	}

	void FixedUpdate(){
        if (!isStunned)
        {
            if (Physics.Raycast(transform.position, current_dir, 0.51f, layer_mask))
            {
                StopCoroutine(co);
                co = StartCoroutine(Move());
            }
            rb.velocity = current_dir * move_speed;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
	}

	IEnumerator Move()
	{
		Vector3 direction;
		int iter = 0;
		while (true) {
			int i = Random.Range (0, 4);
			direction = directions [i];
			
			if (Physics.Raycast (transform.position, direction, 1f, layer_mask) == false) {
				current_dir = direction;
				break;
			}

			// Prevent infinite looping
			iter++;
			if (iter > 100)
				break;
		}
		yield return new WaitForSeconds (change_dir_time);
		co = StartCoroutine (Move ());
	}

    void OnTriggerEnter(Collider coll)
    {
        GameObject other = coll.gameObject;
        if (other.GetComponent<Boomerang>() != null)
        {
            isStunned = true;
            Vector3 temp = rb.velocity;

            rb.velocity = Vector3.zero;

            StartCoroutine(waitForMe());
            rb.velocity = temp;

        }
    }
    IEnumerator waitForMe()
    {
        yield return new WaitForSeconds(3f);
        isStunned = false;
    }
}