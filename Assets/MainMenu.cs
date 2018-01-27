using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public Text[] options;
	public string[] scene_names;
	string[] original_texts;

	int selected = 0;
	// Use this for initialization
	void Start () {
		original_texts = new string[options.Length];

		for (int i = 0; i < options.Length; i++) {
			original_texts [i] = options [i].text;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Tab)) {
			selected++;
			if (selected == options.Length) {
				selected = 0;
			}
		}

		if (Input.GetKeyDown (KeyCode.Return)) {
			SceneManager.LoadScene (scene_names [selected]);
		}
			
		for (int i = 0; i < options.Length; i++) {
			options [i].text = "  " + original_texts [i];
		}

		options [selected].text = "* " + original_texts [selected];
	}
}
