using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class link_boss_animatorController : MonoBehaviour {

    Animator animator;
    ArrowKeyMovement controller;
    public bool isLeft;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        controller = GetComponent<ArrowKeyMovement>();
        isLeft = true;
    }
	
	// Update is called once per frame
	void Update () {
       
        float horizontal_input = Input.GetAxisRaw("Horizontal");
        float vertical_input = Input.GetAxisRaw("Vertical");

        // Prevent confusing the animatior
        if (Mathf.Abs(horizontal_input) > 0.0f)
            vertical_input = 0.0f;

        animator.SetFloat("horizontal_input", horizontal_input);
        animator.SetFloat("vertical_input", vertical_input);

        if(horizontal_input < 0.0f)
        {
            animator.SetBool("isLeft", true);
        }
        else
        {
            animator.SetBool("isLeft", false);
        }

        if (!isLeft && horizontal_input > 0 || isLeft && horizontal_input < 0)
        {
            isLeft = !isLeft;

            Vector3 myScale = transform.localScale;

            myScale.x *= -1;

            transform.localScale = myScale;
        }
        

        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            animator.speed = 0.0f;
        }
        else
        {
            animator.speed = 1.0f;
        }
    }
}
