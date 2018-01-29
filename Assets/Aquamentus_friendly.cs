using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aquamentus_friendly : EnemyController {

	public float reload_time = 1f;
	public float power_up_time = 0f;
	public Transform fireball_spawnpoint;
	public Rigidbody fireball;
	public float fireball_speed = 4f;
    GameObject idle_aquamentus;
    GameObject player;
    GameObject link_with_boss;

    bool canshoot = true;
	bool forward = true;
    bool xEnabled = true;

	Animator anim;
	Rigidbody rb;

	Vector3[] shootdirs = {
		new Vector3 (4, 1, 0).normalized,
		new Vector3 (4, -1, 0).normalized,
		new Vector3 (1, 0, 0)
	};

	Rigidbody[] fireballs = new Rigidbody[3];

    void Awake()
    {
        idle_aquamentus = GameObject.Find("mountable_aquamentus");
        player = GameObject.Find("Player");
        link_with_boss = GameObject.Find("link_ride_boss");

    }

    // Use this for initialization
    void Start () {
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody> ();

	}
	
	// Update is called once per frame
	void Update () {

        float horizontal_input = Input.GetAxisRaw("Horizontal");

        if(horizontal_input > 0 && shootdirs[0].x <0)
        {
            for(int i = 0; i < shootdirs.Length; i++)
            {
                shootdirs[i].x *= -1;
            }
        }
        else if(horizontal_input < 0 && shootdirs[0].x > 0)
        {
            for (int i = 0; i < shootdirs.Length; i++)
            {
                shootdirs[i].x *= -1;
            }
        }

        if (canshoot && Input.GetKeyDown("z") && xEnabled)
        {
            StartCoroutine(Shoot());
            xEnabled = true;
        }

        if (Input.GetKeyDown("x") && canshoot)
        {
            Vector3 link_with_boss_position = link_with_boss.GetComponent<Transform>().position;
            Vector3 link_position = link_with_boss_position + new Vector3(-1.5f, 0, 0);
            Collider[] hitcolliders = Physics.OverlapSphere(link_position, 0.3f);
            if (hitcolliders.Length > 0)
            {
                return;
            }
            Vector3 dragonScale = link_with_boss.GetComponent<Transform>().localScale;
            //dragonScale.x *= -1;
            idle_aquamentus.GetComponent<Transform>().position = link_with_boss_position;
            idle_aquamentus.GetComponent<Transform>().localScale = dragonScale;
            player.GetComponent<Transform>().position = link_position;
            link_with_boss.SetActive(false);
            idle_aquamentus.SetActive(true);
            player.SetActive(true);

        }

    }


	IEnumerator Shoot (){
		canshoot = false;
        xEnabled = false;
        anim.SetBool ("firing", true);

		fireballs [0] = Instantiate (fireball, fireball_spawnpoint.position, transform.rotation);
		fireballs [1] = Instantiate (fireball, fireball_spawnpoint.position, transform.rotation);
		fireballs [2] = Instantiate (fireball, fireball_spawnpoint.position, transform.rotation);

		yield return new WaitForSeconds (power_up_time);

		for (int i = 0; i < fireballs.Length; ++i){
			if (fireballs [i] != null)
				fireballs [i].velocity = shootdirs [i] * fireball_speed;
		}
		anim.SetBool ("firing", false);
		yield return new WaitForSeconds (reload_time);
		canshoot = true;
        

    }

}
