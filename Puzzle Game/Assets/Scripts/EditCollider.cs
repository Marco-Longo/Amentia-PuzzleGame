using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditCollider : MonoBehaviour
{
    public Collider coll;

    void Start()
    {
        coll = GetComponent<Collider>();
    }

    public void OnTriggerExit(Collider other) //after player steps on collider, deactivate it
    {
        if (other.gameObject.CompareTag("Player"))
        {
            coll.enabled = false;
        }
    }
}