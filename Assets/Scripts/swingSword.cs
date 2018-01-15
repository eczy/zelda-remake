using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swingSword : MonoBehaviour {

    Animator animator;
	Rigidbody rb;
    Health playerHealth;
    ArrowKeyMovement arrowkeymovement;

	GameObject WeaponGO;
	GameObject SwordGO;


	public float control_disable_time = 0.5f;
	public float reload_time = 0.5f;
    private float currentHealth;
    private int max_health;
    private ArrowKeyMovement.Direction swingDirection;
	bool reloading = false;
    //private bool pressedX = false;
    // Use this for initialization
    void Awake () {
		rb = GetComponent<Rigidbody> ();
        animator = GetComponent<Animator>();
        playerHealth = GetComponent<Health>();
        arrowkeymovement = GetComponent<ArrowKeyMovement>();
        currentHealth = playerHealth.GetHealth();
        max_health = playerHealth.max_health;
		WeaponGO = GameObject.Find ("Weapon");
		SwordGO = GameObject.Find ("Sword");
		SwordGO.GetComponent<Renderer> ().enabled = false;
		SwordGO.GetComponent<BoxCollider> ().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        //pressedX = false;
        animator.SetBool("pressedX", false);
		changeFaceDirectionBool();
		//SwordGO.GetComponent<Renderer> ().enabled = false;
		//SwordGO.GetComponent<BoxCollider> ().enabled = false;
		if (Input.GetKeyDown("x") && !reloading && (playerHealth.GetHealth()<playerHealth.max_health))
        { 
            animator.SetBool("pressedX", true);
			//Debug.Log ("current health is" + currentHealth);
			//Debug.Log ("max health is" + max_health);
			LinkAttack();

        }

    }

	void LinkAttack(){
		switch(swingDirection){
		case ArrowKeyMovement.Direction.South:
			{
				StartCoroutine (AnimateSword (0f, 0f));
				break;
			}
		case ArrowKeyMovement.Direction.West:
			{
				StartCoroutine (AnimateSword (-90f, 0f));
				break; 
			}
		case ArrowKeyMovement.Direction.East:
			{
				StartCoroutine (AnimateSword (90f, -0.2f));
				break; 
			}
		case ArrowKeyMovement.Direction.North:
			{
				StartCoroutine (AnimateSword (180f, 0f));
				break; 
			}
		}
	}

	IEnumerator AnimateSword(float rotation, float verticalMove){
		
		WeaponGO.transform.rotation = Quaternion.Euler (0, 0, rotation);
		WeaponGO.transform.position = new Vector3 (transform.position.x,
			transform.position.y + verticalMove, transform.position.z);
		arrowkeymovement.DisableControls ();
		reloading = true;
		animator.enabled = false;
		StartCoroutine(Reload ());
		yield return new WaitForSeconds (control_disable_time);
		SwordGO.GetComponent<Renderer> ().enabled = false; 
		animator.enabled = true;
		arrowkeymovement.EnableControls ();
	}

	IEnumerator Reload (){
		SwordGO.GetComponent<Renderer> ().enabled = true;
		SwordGO.GetComponent<BoxCollider> ().enabled = true;
		yield return new WaitForSeconds (reload_time);
		SwordGO.GetComponent<Renderer> ().enabled = false;
		SwordGO.GetComponent<BoxCollider> ().enabled = false;
		reloading = false;
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
