using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swingSword : MonoBehaviour {

    Animator animator;
    Health playerHealth;
    ArrowKeyMovement arrowkeymovement;
    
    
    private float currentHealth;
    private int max_health;
    private ArrowKeyMovement.Direction swingDirection;
    //private bool pressedX = false;
    // Use this for initialization
    void Start () {
        animator = gameObject.GetComponent<Animator>();
        playerHealth = GetComponent<Health>();
        arrowkeymovement = GetComponent<ArrowKeyMovement>();
        currentHealth = playerHealth.GetHealth();
        max_health = playerHealth.max_health;
        swingDirection = arrowkeymovement.linkDirection;
    }
	
	// Update is called once per frame
	void Update () {
        //pressedX = false;
        animator.SetBool("pressedX", false);
        if (Input.GetKeyDown("x"))
        {
            //pressedX = true;
            
            animator.SetBool("pressedX", true);
            Debug.Log("pressedX in animator is " + animator.GetBool("pressedX"));
            //animator.SetFloat("horizontal_input", 0.0f);
            //animator.SetFloat("vertical_input", 0.0f);
            if (currentHealth >= max_health)
            {
                //sword flies

            }
            else
            {
                //basic sword swing
                
            }
        }
        
    }


}
