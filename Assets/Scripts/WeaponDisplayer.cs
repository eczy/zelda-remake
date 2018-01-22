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

		if (w.weapon_A is swingSword)
			weapon_A_name = "Sword";
		else if (w.weapon_A is Bow)
			weapon_A_name = "Bow";
		else if (w.weapon_A is BombUse)
			weapon_A_name = "Bomb";
		else if (w.weapon_A is UseBoomerang)
			weapon_A_name = "Boomerang";
		else
			weapon_A_name = "None";

		if (w.weapon_B is swingSword)
			weapon_B_name = "Sword";
		else if (w.weapon_B is Bow)
			weapon_B_name = "Bow";
		else if (w.weapon_B is BombUse)
			weapon_B_name = "Bomb";
		else if (w.weapon_B is UseBoomerang)
			weapon_B_name = "Boomerang";
		else
			weapon_B_name = "None";

		text_component.text = "Weapon A: " + weapon_A_name + "\nWeapon B: " + weapon_B_name;
	}
}
