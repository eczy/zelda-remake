using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ArrowKeyMovement : MonoBehaviour {

	Rigidbody rb;
    Vector3 temPosYChange;
    Vector3 temPosXChange;


    public float movement_speed = 4;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
        
    }
	
	// Update is called once per frame
	void Update () {
        temPosYChange = transform.position;
        temPosXChange = transform.position;
        fixMovement();
        //float centerX = Mathf.Round(transform.position.x + current_input.x);
        //float centerY = Mathf.Round(transform.position.y + current_input.y);
        //rb.position = new Vector2(centerX, centerY);
        Vector2 current_input = GetInput ();
		rb.velocity = current_input * movement_speed;
        

    }

	Vector2 GetInput(){
		float horizontal_input = Input.GetAxisRaw ("Horizontal");
		float vertical_input = Input.GetAxisRaw ("Vertical");

		if (Mathf.Abs (horizontal_input) > 0.0f) {
			vertical_input = 0.0f;
		}

		return new Vector2 (horizontal_input, vertical_input);
	}

    void fixMovement()
    {
        Vector2 current_input = GetInput();
        if (Mathf.Abs(current_input.x) > 0.0f)
        {
            float decimal_part = transform.position.y % 1;
            Debug.Log("y decimal is "+decimal_part);
            if (decimal_part > 0.7)
            {
                temPosYChange.y = (float)Math.Truncate(transform.position.y) + 1;
            }
            else if(decimal_part < 0.25)
            {
                temPosYChange.y = (float)Math.Truncate(transform.position.y);
            }
            transform.position = temPosYChange;
        }
        else if(Mathf.Abs(current_input.y) > 0.0f)
        {
            float decimal_part = transform.position.x % 1;
            Debug.Log("x decimal is " + decimal_part);
            if (decimal_part > 0.7)
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
