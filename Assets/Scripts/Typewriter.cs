using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Typewriter : MonoBehaviour {
	public float text_speed = 0.5f;
	public TextMesh[] lines;
	AudioSource audio;

	string[] original_text;

	void Start(){
		original_text = new string[lines.Length];
		for (int i = 0; i < lines.Length; i++) {
			original_text [i] = lines [i].text;
			lines [i].text = "";
		}
		audio = GetComponent<AudioSource> ();
	}
	// Use this for initialization
	public IEnumerator Go () {
		audio.enabled = true;
		for (int i = 0; i < lines.Length; i++) {
			for (int j = 0; j < original_text [i].Length; j++) {
				lines [i].text += original_text [i] [j];
				yield return new WaitForSeconds (text_speed);
			}
		}
		audio.enabled = false;
	}
}
