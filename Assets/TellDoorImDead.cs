using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TellDoorImDead : MonoBehaviour {

	public UnlockOnEnemyDeath count;
	void OnDestroy(){
		count.num_alive--;
	}
}