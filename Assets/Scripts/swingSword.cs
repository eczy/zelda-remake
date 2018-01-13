using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swingSword : MonoBehaviour {

    Health playerHealth;
    ArrowKeyMovement arrowkeymovement;

    private float currentHealth;
    private int max_health;
	// Use this for initialization
	void Start () {
        playerHealth = GetComponent<Health>();
        arrowkeymovement = GetComponent<ArrowKeyMovement>();
        currentHealth = playerHealth.GetHealth();
        max_health = playerHealth.max_health;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("x"))
        {
            if(currentHealth >= max_health)
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
