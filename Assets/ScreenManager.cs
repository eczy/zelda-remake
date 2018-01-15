using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour {
	public int x;
	public int y;

	// Use this for initialization
	void Start () {
		Screen.SetResolution (x, y, false);
	}
}
