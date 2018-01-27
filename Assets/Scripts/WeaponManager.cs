using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour {

	public Weapon[] weapons;
	public Weapon weapon_A;
	public Weapon weapon_B;
	public Text weapontext;
	KeyCode[] weapon_keys;
	KeyCode[] mapped_keys;

	void Start () {
		weapon_keys = new KeyCode[] {
			KeyCode.Z,
			KeyCode.X
		};

		mapped_keys = new KeyCode[] {
			KeyCode.Alpha2,
			KeyCode.Alpha3,
			KeyCode.Alpha4,
			KeyCode.Alpha5
		};

		// Default equip sword
		weapons [0].usage_key = weapon_keys [0];
		weapon_A = weapons [0];
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < mapped_keys.Length; i++) {
			if (Input.GetKey (weapon_keys [0]) && Input.GetKey (mapped_keys [i])) {
				if (weapon_A != null)
					weapon_A.usage_key = KeyCode.None;
				weapons [i].usage_key = weapon_keys [0];
				weapon_A = weapons [i];
			}
			if (Input.GetKey (weapon_keys [1]) && Input.GetKey (mapped_keys [i])) {
				if (weapon_B != null)
					weapon_B.usage_key = KeyCode.None;
				weapons [i].usage_key = weapon_keys [1];
				weapon_B = weapons [i];
			}
		}

		if (weapontext == null)
			return;

		string weapon_A_name, weapon_B_name;

		if (weapon_A != null) {
			if (weapon_A is swingSword)
				weapon_A_name = "Sword";
			else if (weapon_A is Bow)
				weapon_A_name = "Bow";
			else if (weapon_A is BombUse)
				weapon_A_name = "Bomb";
			else if (weapon_A is UseBoomerang)
				weapon_A_name = "Boomerang";
			else
				weapon_A_name = "None";
		} else {
			Debug.Log ("Weapon A null");
			weapon_A_name = "None";
		}

		if (weapon_B != null) {
			if (weapon_B is swingSword)
				weapon_B_name = "Sword";
			else if (weapon_B is Bow)
				weapon_B_name = "Bow";
			else if (weapon_B is BombUse)
				weapon_B_name = "Bomb";
			else if (weapon_B is UseBoomerang)
				weapon_B_name = "Boomerang";
			else
				weapon_B_name = "None";
		} else {
			Debug.Log ("Weapon B null");
			weapon_B_name = "None";
		}

		weapontext.text = "Weapon A: " + weapon_A_name + "\nWeapon B: " + weapon_B_name;

	}
}
