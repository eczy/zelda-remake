﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushable_wall : MonoBehaviour {

    Rigidbody rb;
    GameObject playerGO;
    ArrowKeyMovement arrowkeymovement;
    private ArrowKeyMovement.Direction faceDirection;
    private Vector3 originalPos;
    private Vector3 checkVec;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        playerGO = GameObject.Find("Player");
        originalPos = transform.position;
    }

    private void OnCollisionStay(Collision coll)
    {
        if (coll.collider.tag == "Player")
        {
            Vector3 direction = coll.transform.position - transform.position;
            rb.AddForceAtPosition(direction.normalized, transform.position);
            
        }
    }
    private void Update()
    {
        checkVec = originalPos - transform.position;
        if (Mathf.Abs(checkVec.x) >= 1)
        {
            rb.isKinematic = true;
            return;
        }
        if (Mathf.Abs(checkVec.y) >= 1)
        {
            rb.isKinematic = true;
            return;
        }

    }

}