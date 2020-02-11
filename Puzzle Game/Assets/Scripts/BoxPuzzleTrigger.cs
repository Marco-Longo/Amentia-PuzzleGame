using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPuzzleTrigger : MonoBehaviour
{
    public GameObject gm;
    public float force;

    void OnTriggerEnter(Collider other)
    {
        //When the box enters the correct space (measured by a trigger box) increase puzzle completion
        if (other.isTrigger == true && other.CompareTag("boxPuzzleTrigger"))
        {
            gm.GetComponent<GameManager>().IncreasePuzzleCompletion();   
        }
    }

    void OnTriggerExit(Collider other)
    {
        //When the box is no longer in position, decrease puzzle completion
        if (other.isTrigger == true && other.CompareTag("boxPuzzleTrigger"))
        {
            gm.GetComponent<GameManager>().DecreasePuzzleCompletion();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("The player pushed me");
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();

            //Evaluate hit direction
            Vector3 direction = (transform.position - collision.gameObject.transform.position).normalized;
            Ray ray = new Ray(collision.gameObject.transform.position, direction);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            Vector3 normal = hit.normal;

            //Apply forces in the correct directions on both the box and the player
            rb.AddForce(-normal * force, ForceMode.Impulse);
            collision.gameObject.GetComponent<Rigidbody>().AddForce(normal * force/2.5f, ForceMode.Impulse);

            /*
            if (normal == transform.forward) //Front Collision
            {
                rb.AddForce(Vector3.forward * force, ForceMode.Impulse);
            }
            else if (normal == -transform.forward) //Back Collision
            {
                
            }
            else if (normal == transform.right) //Right Collision
            {
                
            }
            else if (normal == -transform.right) //Left Collision
            {
                
            }
            */
        }
    }
}



