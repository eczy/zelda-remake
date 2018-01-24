using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swingSword : Weapon {
    Animator animator;
	Rigidbody rb;
    Health playerHealth;
    ArrowKeyMovement arrowkeymovement;

	GameObject WeaponGO;
	GameObject SwordGO;

	public AudioClip full_health_use_sound;
	public AudioClip use_sound;
	public Sprite shoot_up_sprite;
	public Sprite shoot_down_sprite;
	public Sprite shoot_left_sprite;
	public Sprite shoot_right_sprite;
	SpriteRenderer renderer;
	public float control_disable_time = 0.2f;
	public float reload_time = 0.1f;
    private float currentHealth;
    private float max_health;
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

        

        renderer = GetComponent<SpriteRenderer> ();
    }
	
	// Update is called once per frame
	void Update () {
        animator.SetBool("pressedX", false);
		changeFaceDirectionBool();
		if (Input.GetKeyDown(usage_key) && !reloading && (playerHealth.GetHealth()<playerHealth.max_health))
        { 
            animator.SetBool("pressedX", true);
			AudioSource.PlayClipAtPoint (use_sound, Camera.main.transform.position);
			LinkAttack();
        }


    }

    

	void LinkAttack(){
		Sprite original_sprite = renderer.sprite;
		switch(swingDirection){
		case ArrowKeyMovement.Direction.South:
			{
				StartCoroutine (AnimateSword (0f, 0f,original_sprite,shoot_down_sprite));
				break;
			}
		case ArrowKeyMovement.Direction.West:
			{
				StartCoroutine (AnimateSword (-90f, 0f,original_sprite,shoot_left_sprite));
				break; 
			}
		case ArrowKeyMovement.Direction.East:
			{
				StartCoroutine (AnimateSword (90f, -0.2f,original_sprite,shoot_right_sprite));
				break; 
			}
		case ArrowKeyMovement.Direction.North:
			{
				StartCoroutine (AnimateSword (180f, 0f,original_sprite,shoot_up_sprite));
				break; 
			}
		}
	}

	IEnumerator AnimateSword(float rotation, float verticalMove, Sprite original_sprite, Sprite replacement_sprite){
		renderer.sprite = replacement_sprite;
		WeaponGO.transform.rotation = Quaternion.Euler (0, 0, rotation);
		WeaponGO.transform.position = new Vector3 (transform.position.x,
			transform.position.y + verticalMove, transform.position.z);
		arrowkeymovement.DisableControls ();
		reloading = true;
		animator.enabled = false;
		StartCoroutine(Reload ());
		yield return new WaitForSeconds (control_disable_time);
		renderer.sprite = original_sprite;
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
