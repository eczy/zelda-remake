using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashWhenDamaged : MonoBehaviour {
	SpriteRenderer spr;

	Coroutine cor;

	void Start ()
	{
		spr = GetComponent<SpriteRenderer> ();
	}

	public void Flash(float time)
	{
		cor = StartCoroutine (FlashAnim (time));
	}

	IEnumerator FlashAnim(float time)
	{
		spr.color = Color.red;
		yield return new WaitForSeconds(time);
		spr.color = Color.white;
	}
}
