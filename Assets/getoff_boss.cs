using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getoff_boss : MonoBehaviour {

    GameObject idle_aquamentus;
    GameObject player;
    GameObject link_with_boss;

    void Awake()
    {
        idle_aquamentus = GameObject.Find("mountable_aquamentus");
        player = GameObject.Find("Player");
        link_with_boss = GameObject.Find("link_ride_boss");
    }

    // Use this for initialization
    void Start () {
        //player.SetActive(false);
        //idle_aquamentus.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("x"))
        {
            Vector3 link_with_boss_position = link_with_boss.GetComponent<Transform>().position;
            Vector3 link_position = link_with_boss_position + new Vector3(-1.5f,0,0);
            Vector3 dragonScale = link_with_boss.GetComponent<Transform>().localScale;
            dragonScale.x *= -1;
            idle_aquamentus.GetComponent<Transform>().position = link_with_boss_position;
            idle_aquamentus.GetComponent<Transform>().localScale = dragonScale;
            player.GetComponent<Transform>().position = link_position;
            link_with_boss.SetActive(false);
            idle_aquamentus.SetActive(true);
            player.SetActive(true);
        }
	}
}
