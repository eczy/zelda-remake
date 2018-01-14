using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class enemyMovement : MonoBehaviour {

    public float movement_speed = 3f;
    public float adjustPosition_force = 0.5f;
    public Vector2 direction = Vector2.left;
    Vector3 temPosYChange;
    Vector3 temPosXChange;
    private int randomNumber;
    private int previousRandom;
    private float xDirection;
    private float yDirection;
    Rigidbody rb;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        //temPosYChange = transform.position;
        // temPosXChange = transform.position;
        // FixEnemyMovement();
        xDirection = direction.x;
        yDirection = direction.y;
        rb.velocity = direction * movement_speed;
        Debug.Log(direction.ToString());
    }

  

    private void OnCollisionStay(Collision coll)
    {
        if (xDirection == -1)
        {
            Debug.Log("inside xDirection left");
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
                transform.localScale = new Vector2(-1 * transform.localScale.x, transform.localScale.y);
                direction = new Vector2(-1 * direction.x, direction.y);
            }
            
        }
        if(xDirection == 1)
        {
            Debug.Log("inside xDirection right");
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
            Debug.Log("inside yDirection right");
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
            Debug.Log("inside yDirection left");
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
}
