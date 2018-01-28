using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class friendly_Fireball : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }

}
