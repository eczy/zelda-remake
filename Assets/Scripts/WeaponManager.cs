using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {

	public Weapon[] weapons;

	public Weapon weapon_A;
	public Weapon weapon_B;
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
	}
}
