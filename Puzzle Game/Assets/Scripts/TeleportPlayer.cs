using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    public GameObject player;
    public GameObject pivot;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;
        
        Vector3 newPos = new Vector3(player.transform.position.x, player.transform.position.y, pivot.transform.position.z);
        player.GetComponent<PlayerController>().SetPosition(newPos);
    }
}
