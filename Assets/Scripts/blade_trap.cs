using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blade_trap : MonoBehaviour {

    public float upRay;
    public float leftRay;
	// Use this for initialization
	void Start () {
        upRay = 2.8f;
        leftRay = 5f;
    }
	
	// Update is called once per frame
	void Update () {
    //    Vector3 dir = new Vector3(0, 1, 0);
    //    Debug.DrawRay(transform.position, dir, Color.green);
    //    if (Physics.Raycast(transform.position, dir, 25))
            
    //        print("There is something in front of the object!");
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        Vector3 dirUp = new Vector3(0, 1, 0);
        Vector3 dirLeft = new Vector3(-1, 0, 0);
        Debug.DrawRay(transform.position, dirUp* upRay, Color.green);
        Debug.DrawRay(transform.position, dirLeft * leftRay, Color.green);
        if (Physics.Raycast(transform.position, dirUp, out hit, upRay))
        {
            if (hit.collider.tag == "Player")
            {
                print("Found an object - distance: " + hit.distance);
            }
        }
        else if(Physics.Raycast(transform.position, dirLeft, out hit, leftRay))
            {
            if (hit.collider.tag == "Player")
            {
                print("Found an object - distance: " + hit.distance);
            }
        }
            
    }
}
