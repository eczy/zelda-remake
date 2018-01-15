using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class enemyMovement : MonoBehaviour {

    public float movement_speed = 3f;
    public float adjustPosition_force = 0.5f;
	public Vector3 direction = new Vector3 (1, 0, 0);
    private int randomNumber;
    private int previousRandom;
    Rigidbody rb;

	Vector3 temPosYChange;
	Vector3 temPosXChange;

	void Start () {
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        rb.velocity = direction * movement_speed;
	}

	void Update () {
		Vector3 current_pos = transform.position;
		Debug.Log (direction.ToString ());
		if (direction.y < 0.1) {
			transform.position = new Vector3 (current_pos.x, Mathf.RoundToInt (current_pos.y), current_pos.z);
		} else {
			transform.position = new Vector3 (Mathf.RoundToInt (current_pos.x), current_pos.y, current_pos.z);
		}
	}

	/*
    private void OnCollisionStay(Collision coll)
    {
		if (direction.x < 0)
        {
            System.Random r = new System.Random();
            randomNumber = r.Next(1, 4); // creates a number between 1 and 3
            
            if (randomNumber == 1)
            {
                //go up
                direction = new Vector2(0,1);
            }
            else if (randomNumber == 2)
            {
                //go down
                direction = new Vector2(0,-1);
            }
            else
            {
                //go right
                //transform.localScale = new Vector2(-1 * transform.localScale.x, transform.localScale.y);
                //direction = new Vector2(-1 * direction.x, direction.y);
				direction = new Vector2(1, 0);
            }
        }
        if(xDirection == 1)
        {
            //Debug.Log("inside xDirection right");
            System.Random r = new System.Random();
            randomNumber = r.Next(1, 4); // creates a number between 1 and 3
            if (randomNumber == 1)
            {
                //go up
                direction = new Vector2(0, 1);
            }
            else if (randomNumber == 2)
            {
                //go down
                direction = new Vector2(0, -1);
            }
            else
            {
                //go left
                transform.localScale = new Vector2(-1 * transform.localScale.x, transform.localScale.y);
                direction = new Vector2(-1 * direction.x, direction.y);
            }
        }
        if(yDirection == 1)
        {
            //Debug.Log("inside yDirection right");
            System.Random r = new System.Random();
            randomNumber = r.Next(1, 4); // creates a number between 1 and 3
            if (randomNumber == 1)
            {
                //go left
                direction = new Vector2(-1, 0);
            }
            else if (randomNumber == 2)
            {
                //go down
                direction = new Vector2(0, -1);
            }
            else
            {
                //go right
                direction = new Vector2(1, 0);
            }
        }
        if(yDirection == -1)
        {
            //Debug.Log("inside yDirection left");
            System.Random r = new System.Random();
            randomNumber = r.Next(1, 4); // creates a number between 1 and 3
            if (randomNumber == 1)
            {
                //go left
                direction = new Vector2(-1, 0);
            }
            else if (randomNumber == 2)
            {
                //go up
                direction = new Vector2(0, 1);
            }
            else
            {
                //go right
                direction = new Vector2(1, 0);
            }
        }
        rb.AddForce(direction * adjustPosition_force, ForceMode.Impulse);

    }
    */

	void OnCollisionStay(Collision coll)
	{
		GameObject other = coll.collider.gameObject;

		//Vector3 pos = transform.position;
		//transform.position = new Vector3 (Mathf.RoundToInt (pos.x), Mathf.RoundToInt (pos.y), Mathf.RoundToInt (pos.z));

		Vector3 dir = transform.position - other.transform.position;
		if (Mathf.Abs (dir.x) > Mathf.Abs (dir.y)) {
			if (dir.x >= 0)
				dir = new Vector3 (1, 0, 0);
			else
				dir = new Vector3 (-1, 0, 0);
		} else {
			if (dir.y >= 0)
				dir = new Vector3 (0, 1, 0);
			else
				dir = new Vector3 (0, -1, 0);
		}

		int r = UnityEngine.Random.Range (0, 3);

		switch (r) {
		case (0):
			direction = Quaternion.Euler (0, 0, 90) * dir;
			break;
		case (1):
			direction = Quaternion.Euler (0, 0, 180) * dir;
			break;
		case (2):
			direction = Quaternion.Euler (0, 0, 270) * dir;
			break;
		default:
			direction = Quaternion.Euler (0, 0, 180) * dir;
			break;
		}

		rb.AddForce (direction * movement_speed, ForceMode.Impulse);
	}

	/*

    void FixEnemyMovement()
    {
        if (Mathf.Abs(transform.position.x) > 0.0f)
        {
            float decimal_part = transform.position.y % 1;

            if (decimal_part > 0.6)
            {
                temPosYChange.y = (float)Math.Truncate(transform.position.y) + 1;
            }
            else if (decimal_part < 0.25)
            {
                temPosYChange.y = (float)Math.Truncate(transform.position.y);
            }
            transform.position = temPosYChange;
        }
        if (Mathf.Abs(transform.position.y) > 0.0f)
        {
            float decimal_part = transform.position.x % 1;

            if (decimal_part > 0.6)
            {
                temPosXChange.x = (float)Math.Truncate(transform.position.x) + 1;
            }
            else if (decimal_part < 0.25)
            {
                temPosXChange.x = (float)Math.Truncate(transform.position.x);
            }
            transform.position = temPosXChange;
        }
        return;
    }
    */
}
