using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TellItemImDead : MonoBehaviour {

	public AppearOnEnemyKill count;
	void OnDestroy(){
		count.num_alive--;
	}
}
