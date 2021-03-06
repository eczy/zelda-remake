﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseBoomerang : Weapon {

    Animator animator;
    Rigidbody rb;
    Health playerHealth;
    ArrowKeyMovement arrowkeymovement;
    SpriteRenderer renderer;
    GameObject BoomerangGO;
    GameObject PlayerGO;

    public Sprite shoot_up_sprite;
    public Sprite shoot_down_sprite;
    public Sprite shoot_left_sprite;
    public Sprite shoot_right_sprite;
	public AudioClip use_sound;

    private ArrowKeyMovement.Direction swingDirection;
    private float animationTime = 1.4f;
    private float lastTime;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerHealth = GetComponent<Health>();
        arrowkeymovement = GetComponent<ArrowKeyMovement>();

        BoomerangGO = GameObject.Find("Boomerang");
        PlayerGO = GameObject.Find("Player");

        BoomerangGO.GetComponent<Renderer>().enabled = false;
        BoomerangGO.GetComponent<SphereCollider>().enabled = false;
        renderer = GetComponent<SpriteRenderer>();

        lastTime = Time.time;
    }

    void Update()
    {
        swingDirection = arrowkeymovement.linkDirection;
        
        if (Input.GetKeyDown(usage_key) && (Time.time - lastTime)>animationTime)
        {
            //AudioSource.PlayClipAtPoint (use_sound, Camera.main.transform.position);
            BoomerangGO.GetComponent<SphereCollider>().enabled = true;
            BoomerangAttack();
            
            lastTime = Time.time;
        }
    }

    void BoomerangAttack()
    {
		AudioSource.PlayClipAtPoint (use_sound, Camera.main.transform.position);
        Sprite original_sprite = renderer.sprite;
        switch (swingDirection)
        {
            case ArrowKeyMovement.Direction.South:
                {
                    BoomerangGO.GetComponent<Renderer>().enabled = true;
                    BoomerangGO.GetComponent<Animator>().enabled = true;
                    BoomerangGO.GetComponent<Animator>().Play("boomerang_down");
                    StartCoroutine(AnimateBoomerang(original_sprite, shoot_down_sprite));
                    break;
                }
            case ArrowKeyMovement.Direction.West:
                {
                    BoomerangGO.GetComponent<Renderer>().enabled = true;
                    BoomerangGO.GetComponent<Animator>().enabled = true;
                    BoomerangGO.GetComponent<Animator>().Play("boomerang_left");
                    StartCoroutine(AnimateBoomerang(original_sprite, shoot_left_sprite));
                    break;
                }
            case ArrowKeyMovement.Direction.East:
                {
                    BoomerangGO.GetComponent<Renderer>().enabled = true;
                    BoomerangGO.GetComponent<Animator>().enabled = true;
                    BoomerangGO.GetComponent<Animator>().Play("boomerang_right");
                    StartCoroutine(AnimateBoomerang(original_sprite, shoot_right_sprite));
                    break;
                }
            case ArrowKeyMovement.Direction.North:
                {
                    BoomerangGO.GetComponent<Renderer>().enabled = true;
                    BoomerangGO.GetComponent<Animator>().enabled = true;
                    BoomerangGO.GetComponent<Animator>().Play("boomerang_up");
                    StartCoroutine(AnimateBoomerang(original_sprite, shoot_up_sprite));
                    break;
                }
        }
    }

    IEnumerator AnimateBoomerang(Sprite original_sprite, Sprite replacement_sprite)
    {
        renderer.sprite = replacement_sprite;
        animator.enabled = false;
        yield return new WaitForSeconds(0.1f);
        renderer.sprite = original_sprite;
        animator.enabled = true;
        yield return new WaitForSeconds(1.2f);
        BoomerangGO.GetComponent<Animator>().enabled = false;
        BoomerangGO.GetComponent<SphereCollider>().enabled = false;
        BoomerangGO.GetComponent<Renderer>().enabled = false;
    }
}
