using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour
{

    public GameObject holePrefab;

    void OnCollisionEnter(Collision collision)
    {
        GameObject hole = Instantiate(holePrefab, transform.position, Quaternion.identity);
        hole.transform.parent = GameObject.FindWithTag("Ship Shell").transform;
        GameManager.Instance.DoPlayerDamage();
        Destroy(gameObject);
    }
}
