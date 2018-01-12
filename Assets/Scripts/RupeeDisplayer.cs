using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RupeeDisplayer : MonoBehaviour {

	public Inventory inventory;
	Text text_component;

	void Start(){
		text_component = GetComponent<Text> ();
	}

	void Update(){
		if (inventory != null && text_component != null) {
			text_component.text = inventory.GetRupees ().ToString ();
		}
	}
}
