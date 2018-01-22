using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

	public SpriteRenderer bomb_cloud;
	public Sprite cloud_full;
	public Sprite cloud_half;
	public Sprite cloud_quarter;
	public float fuse_time = 1f;
	public float cloud_disperse_time = 1f;

	// Use this for initialization
	IEnumerator Start ()
	{
		yield return new WaitForSeconds (fuse_time);
		GetComponent<SpriteRenderer> ().enabled = false;
		GetComponent<SphereCollider> ().enabled = true;
		SpriteRenderer[] clouds = {
			Instantiate (bomb_cloud, transform.position + new Vector3 (-0.5f, 0.5f, 0f), transform.rotation),
			Instantiate (bomb_cloud, transform.position + new Vector3 (0f, 0f, 0f), transform.rotation),
			Instantiate (bomb_cloud, transform.position + new Vector3 (0.5f, -0.5f, 0f), transform.rotation),
			Instantiate (bomb_cloud, transform.position + new Vector3 (0.75f, 0f, 0f), transform.rotation)
		};

		foreach (SpriteRenderer cloud in clouds){
			cloud.sprite = cloud_full;
		}

		yield return new WaitForSeconds (cloud_disperse_time / 2);

		foreach (SpriteRenderer cloud in clouds) {
			cloud.sprite = cloud_half;
		}

		yield return new WaitForSeconds (cloud_disperse_time / 4);

		foreach (SpriteRenderer cloud in clouds) {
			cloud.sprite = cloud_quarter;
		}

		yield return new WaitForSeconds (cloud_disperse_time / 4);

		foreach (SpriteRenderer cloud in clouds) {
			Destroy (cloud.gameObject);
		}

		Destroy (gameObject);
	}
}
