using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public GameObject endGameScreen;
    public GameObject monster1;
    public GameObject monster2;
    public AudioClip endGameSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager audio = AudioManager.Instance;
            audio.GetComponent<AudioSource>().Stop();
            audio.GetComponent<AudioSource>().clip = endGameSound;
            audio.GetComponent<AudioSource>().Play();
            monster1.GetComponent<AudioSource>().Stop();
            monster2.GetComponent<AudioSource>().Stop();
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            endGameScreen.SetActive(true);
        }
    }
}