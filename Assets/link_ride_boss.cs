using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class link_ride_boss : MonoBehaviour {

    GameObject idle_aquamentus;
    GameObject player;
    GameObject link_with_boss;
    

	// Use this for initialization
	void Awake () {
        idle_aquamentus = GameObject.Find("mountable_aquamentus");
        player = GameObject.Find("Player");
        link_with_boss = GameObject.Find("link_ride_boss");
    }

    private void Start()
    {
        Vector3 idle_aquamentus_position = GetComponent<Transform>().position;
        link_with_boss.SetActive(false);
        link_with_boss.GetComponent<Transform>().position = idle_aquamentus_position;
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.tag == "Player")
        {
            idle_aquamentus.SetActive(false);
            player.SetActive(false);
            link_with_boss.SetActive(true);
        }
    }
}
