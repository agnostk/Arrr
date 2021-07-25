using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    CANNONBALL,
    WOODEN_PLANK
}

public class Item : Interactable
{
    public ItemType itemType;

    private Rigidbody rb;
    private Collider col;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    public void EnableSimulation()
    {
        transform.parent = null;
        rb.isKinematic = false;
        col.enabled = true;
    }

    public void DisableSimulation()
    {
        rb.isKinematic = true;
        col.enabled = false;
    }
}
