using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {

    Camera main_camera;

    void Start()
    {
        main_camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        if (main_camera == null)
        {
            Debug.LogWarning("WARNING: Could not find main camera");
            Destroy(this);
        }
    }

    void Update()
    {
        Vector3 pos = main_camera.WorldToViewportPoint(transform.position);
        if (pos.x < 0 || pos.x > 1 || pos.y < 0 || pos.y > 1)
            Destroy(gameObject);
    }
}
