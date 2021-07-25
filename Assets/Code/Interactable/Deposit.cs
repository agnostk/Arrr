using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deposit : Interactable
{
    [SerializeField] private GameObject item;

    public GameObject GetItem()
    {
        return Instantiate(item, transform.position, Quaternion.identity);
    }
}
