using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swingSword : MonoBehaviour {

    Animator animator;
    Health playerHealth;
    ArrowKeyMovement arrowkeymovement;
	Coroutine co_pause_controls;

	public float control_disable_time = 0.1f;

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
        
    }
	
	// Update is called once per frame
	void Update () {
        //pressedX = false;
        animator.SetBool("pressedX", false);
		changeFaceDirectionBool();
        if (Input.GetKeyDown("x"))
        {
            //pressedX = true;
            animator.SetBool("pressedX", true);
			Debug.Log ("current health is" + currentHealth);
			Debug.Log ("max health is" + max_health);
			co_pause_controls = StartCoroutine (PauseControls());
            //animator.SetFloat("horizontal_input", 0.0f);
            //animator.SetFloat("vertical_input", 0.0f);
            if (currentHealth >= max_health)
            {
                //sword flies
				Debug.Log("damaging beam!");
            }
            else
            {
				//basic sword swing
				if (animator.GetBool ("faceSouth")) {
					
				}
				else if(animator.GetBool ("faceNorth")) {

				}
				else if(animator.GetBool ("faceEast")) {

				}
				else if(animator.GetBool ("faceWest")) {

				}
                
            }
        }

    }

	//heal up link by 1 if he picks up hearts
	void OnTriggerEnter (Collider coll){
		if (coll.gameObject.tag.Equals("heart")==true) {
			Debug.Log ("setting health");
			currentHealth++;
			playerHealth.SetHealth (currentHealth);
		}
	}

	void changeFaceDirectionBool(){
		swingDirection = arrowkeymovement.linkDirection;
		if (swingDirection.ToString () == "South") {
			animator.SetBool("faceNorth", false);
			animator.SetBool("faceWest", false);
			animator.SetBool("faceEast", false);
			animator.SetBool("faceSouth", true);
		}
		else if(swingDirection.ToString () == "North") {
			animator.SetBool("faceNorth", true);
			animator.SetBool("faceWest", false);
			animator.SetBool("faceEast", false);
			animator.SetBool("faceSouth", false);
		}
		else if(swingDirection.ToString () == "West") {
			animator.SetBool("faceNorth", false);
			animator.SetBool("faceWest", true);
			animator.SetBool("faceEast", false);
			animator.SetBool("faceSouth", false);
		}
		else if(swingDirection.ToString () == "East") {
			animator.SetBool("faceNorth", false);
			animator.SetBool("faceWest", false);
			animator.SetBool("faceEast", true);
			animator.SetBool("faceSouth", false);
		}
	}

	IEnumerator PauseControls()
	{
		arrowkeymovement.DisableControls ();
		yield return new WaitForSeconds (control_disable_time);
		arrowkeymovement.EnableControls ();
	}

}
