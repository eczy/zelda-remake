using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponDisplayer : MonoBehaviour {

	public WeaponManager w;
	Text text_component;

	// Use this for initialization
	void Start ()
	{
		text_component = GetComponent <Text> ();
	}

	// Update is called once per frame
	void Update ()
	{
		if (w == null || text_component == null)
			return;

		string weapon_A_name, weapon_B_name;

		if (w.weapon_A != null) {
			if (w.weapon_A.GetComponent<swingSword> () != null)
				weapon_A_name = "Sword";
			else if (w.weapon_A.GetComponent<Bow> () != null)
				weapon_A_name = "Bow";
			else if (w.weapon_A.GetComponent<BombUse> () != null)
				weapon_A_name = "Bomb";
			else if (w.weapon_A.GetComponent<UseBoomerang> () != null)
				weapon_A_name = "Boomerang";
			else
				weapon_A_name = "None";
		} else {
			Debug.Log ("Weapon A null");
			weapon_A_name = "None";
		}

		if (w.weapon_B != null) {
			if (w.weapon_B.GetComponent<swingSword> () != null)
				weapon_B_name = "Sword";
			else if (w.weapon_B.GetComponent<Bow> () != null)
				weapon_B_name = "Bow";
			else if (w.weapon_B.GetComponent<BombUse> () != null)
				weapon_B_name = "Bomb";
			else if (w.weapon_B.GetComponent<UseBoomerang> () != null)
				weapon_B_name = "Boomerang";
			else
				weapon_B_name = "None";
		} else {
			Debug.Log ("Weapon B null");
			weapon_B_name = "None";
		}
		

		text_component.text = "Weapon A: " + weapon_A_name + "\nWeapon B: " + weapon_B_name;
	}
}
