using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swingSword : MonoBehaviour {

    Animator animator;
	Rigidbody rb;
    Health playerHealth;
    ArrowKeyMovement arrowkeymovement;
	Coroutine co_pause_controls;
	GameObject WeaponGO;
	GameObject SwordGO;


	public float control_disable_time = 0.1f;

    private float currentHealth;
    private int max_health;
    private ArrowKeyMovement.Direction swingDirection;
    //private bool pressedX = false;
    // Use this for initialization
    void Awake () {
		rb = GetComponent<Rigidbody> ();
        animator = gameObject.GetComponent<Animator>();
        playerHealth = GetComponent<Health>();
        arrowkeymovement = GetComponent<ArrowKeyMovement>();
        currentHealth = playerHealth.GetHealth();
        max_health = playerHealth.max_health;
		WeaponGO = GameObject.Find ("Weapon");
		SwordGO = GameObject.Find ("Sword");
    }
	
	// Update is called once per frame
	void Update () {
        //pressedX = false;
        animator.SetBool("pressedX", false);
		changeFaceDirectionBool();
        if (Input.GetKey("x"))
        { 
            animator.SetBool("pressedX", true);
			//Debug.Log ("current health is" + currentHealth);
			//Debug.Log ("max health is" + max_health);
			LinkAttack();

        }

    }

	void LinkAttack(){
		switch(swingDirection){

		}
	}

	IEnumerator AnimateSword(float rotation, float verticalMove){
		SwordGO.GetComponent<Renderer> ().enabled = true;
		WeaponGO.transform.rotation = Quaternion.Euler (0, 0, rotation);
		WeaponGO.transform.position = new Vector3 (transform.position.x,
			transform.position.y + verticalMove, transform.position.z);
		arrowkeymovement.DisableControls ();
		yield return new WaitForSeconds (control_disable_time);
		SwordGO.GetComponent<Renderer> ().enabled = false; 
		arrowkeymovement.EnableControls ();
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

}
