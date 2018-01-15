using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplayer : MonoBehaviour {

	public Health health;
	Text text_component;

	// Use this for initialization
	void Start ()
	{
		text_component = GetComponent <Text> ();
	}

	// Update is called once per frame
	void Update ()
	{
		if (health != null && text_component != null)
			text_component.text = "Health: " + health.GetHealth () + "/" + health.max_health;
	}
}
