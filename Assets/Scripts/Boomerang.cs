using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour {

    public bool stunned = false;
    Rigidbody enemyRB;
    Vector3 dir;
    Vector3 preDir;
    private void Start()
    {
        
    }

    private void Update()
    {
        if (!stunned)
        {
            enemyRB.velocity = dir;
            preDir = dir;
        }
        else
        {
            StartCoroutine(UnstunnEnemy());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "stalfo")
        {
            enemyRB = other.GetComponent<Rigidbody>();
            dir = transform.position - other.transform.position;
            if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            {
                if (dir.x >= 0)
                    dir = new Vector3(1, 0, 0);
                else
                    dir = new Vector3(-1, 0, 0);
            }
            else
            {
                if (dir.y >= 0)
                    dir = new Vector3(0, 1, 0);
                else
                    dir = new Vector3(0, -1, 0);
            }
            stunned = true;
        }
    }

    IEnumerator UnstunnEnemy()
    {
        enemyRB.velocity = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(3.0f);
        enemyRB.velocity = preDir;
        stunned = false;
    }
}
