﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

	void OnCollisionEnter(Collision coll)
	{
		Destroy (gameObject);
	}
}
