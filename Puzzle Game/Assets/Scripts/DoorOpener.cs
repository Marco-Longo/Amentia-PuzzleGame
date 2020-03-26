using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    private Animator anim;
    private AudioSource doorSFX;

    public AudioClip doorOpening;
    public AudioClip doorClosing;

    void Start()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
        doorSFX = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            anim.SetBool("isOpen", true);
            if (other.gameObject.GetComponent<PlayerController>().GetPosition().z < transform.position.z)
                anim.SetBool("Reverse", false);
            else
                anim.SetBool("Reverse", true);

            doorSFX.clip = doorOpening;
            doorSFX.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            anim.SetBool("isOpen", false);
            doorSFX.clip = doorClosing;
            doorSFX.PlayDelayed(1.5f);
        }
    }
}
