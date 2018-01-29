using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransitionTriggerDeleteBoss : MonoBehaviour {
	public Vector3 transition_direction;
    public link_ride_boss link_ride_boss;

    private void Awake()
    {
        link_ride_boss = GetComponent<link_ride_boss>();
    }

    void Start()
	{
		transition_direction.Normalize ();
        Instantiate(link_ride_boss, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
