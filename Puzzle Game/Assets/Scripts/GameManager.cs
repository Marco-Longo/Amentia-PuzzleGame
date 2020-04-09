using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public RectTransform pauseMenu;
    public GameObject trapdoorRot;
    public GameObject trapdoorTrigger;
    public GameObject fakeWall;
    public GameObject realDoor;
    public GameObject finalDoor;
    public Slider insanityMeter;
    public Image insanityEffect;

    //Box Puzzle
    private int boxPuzzleCompletion = 0;
    private int boxesCount = 3;

    //Insanity Variables
    private static float insanity = 0.0f;
//  private float decayTimer = 0.0f;
//  private bool inDanger = false;

    //Sound Effects
    public AudioSource soundSource;
    public AudioClip menuSelect;
    public AudioClip menuMovement;
    public AudioClip puzzleCorrect;
    public AudioClip puzzleComplete;
    public AudioClip landingSound;
    public AudioClip leverSound;

    private void Awake()
    {
        //Initialize music and sound
        if (AudioManager.Instance != null)
        {
            if (!AudioManager.Instance.GetComponent<AudioSource>().isPlaying)
                AudioManager.Instance.GetComponent<AudioSource>().Play();
            AudioManager.Instance.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MUSIC");
        }
        soundSource.volume = PlayerPrefs.GetFloat("SOUND");
        
        //Initialize insanity indicators
        insanityMeter.value = insanity;
        insanityEffect.color = new Color(insanityEffect.color.r, insanityEffect.color.g, insanityEffect.color.b, insanity - 0.2f);   
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            OpenMenu();

//      decayTimer += Time.deltaTime;
//      if (!inDanger && insanity > 0.0f && decayTimer > 3.0f)
//          DecreaseInsanity();
    }

    //Box Puzzle Functions
    public void IncreasePuzzleCompletion()
    {
        boxPuzzleCompletion++;
        if (boxPuzzleCompletion == boxesCount) //when all 3 boxes are in position...
        {
            //... open the trapdoor
            trapdoorRot.GetComponent<Animator>().SetBool("Open", true);
            trapdoorRot.GetComponent<AudioSource>().volume *= PlayerPrefs.GetFloat("SOUND");
            if (!trapdoorTrigger.activeInHierarchy)
                trapdoorRot.GetComponent<AudioSource>().Play();
            trapdoorTrigger.gameObject.SetActive(true);
            soundSource.PlayOneShot(puzzleComplete);
        }
        else
            soundSource.PlayOneShot(puzzleCorrect);
    }
    public void DecreasePuzzleCompletion()
    {
        boxPuzzleCompletion--;
    }

    //Monster Insanity Functions
    public void IncreaseInsanity(float amount)
    {
        if (insanity < 1.0f)
        {
//          inDanger = true;
//          decayTimer = 0.0f;
            insanity += amount;
            insanityMeter.value = insanity;
            insanityEffect.color = new Color(insanityEffect.color.r, insanityEffect.color.g, insanityEffect.color.b, insanity - 0.2f);
        }
        if (insanity > 0.5f && !realDoor.activeInHierarchy)
        {
            fakeWall.GetComponent<Animator>().SetBool("Insane", true);
            StartCoroutine(DoorFadeIn());
        }
    }

    private IEnumerator DoorFadeIn()
    {
        realDoor.SetActive(true);
        Color c = realDoor.GetComponent<MeshRenderer>().material.color;
        c.a = 0.0f;
        realDoor.GetComponent<MeshRenderer>().material.color = c;
        realDoor.GetComponent<Animator>().SetBool("Insane", true);

        yield return new WaitForSeconds(4.0f);
        realDoor.GetComponent<BoxCollider>().enabled = true;
    }

    //Cages Insanity Functions
    public void IncreaseInsanityCages(float amount)
    {
        insanity += amount;
        insanityMeter.value = insanity;
        insanityEffect.color = new Color(insanityEffect.color.r, insanityEffect.color.g, insanityEffect.color.b, insanity - 0.2f);

        /*
        if (insanity > .9f) //if insanity is too high reset level
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        */
        //Activate the door when a certain amount of insanity is reached
        if (insanity > 0.35f && insanity < 0.45f)
            finalDoor.SetActive(false);
        else
            finalDoor.SetActive(true);
    }

    public void ResetInsanity()
    {
        //Flowers set insanity to 0 and close the door
        insanity = 0.0f;
        insanityMeter.value = insanity;
        insanityEffect.color = new Color(insanityEffect.color.r, insanityEffect.color.g, insanityEffect.color.b, 0.0f);
        finalDoor.SetActive(true);
    }

    private void DecreaseInsanity()
    {
//      decayTimer = 0.0f;
        insanity -= 0.05f;
        insanityMeter.value = insanity;
    }

    /*
    public void InsanityDecay()
    {
        inDanger = false;
    }
    */

    //Play sound effects
    public void PlayLandingSound()
    {
        soundSource.PlayOneShot(landingSound);
    }
    
    public void PlayLeverSound()
    {
        soundSource.PlayOneShot(leverSound);
    }

    //Pause Menu Functions
    public void OpenMenu()
    {
        soundSource.PlayOneShot(menuMovement);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseMenu.gameObject.SetActive(true);
    }
    public void ResumeGame()
    {
        soundSource.PlayOneShot(menuSelect);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenu.gameObject.SetActive(false);
    }
    public void QuitGame()
    {
        soundSource.PlayOneShot(menuMovement);
        Time.timeScale = 1;
        insanity = 0; //Since insanity is a static variable, it needs to be reset at every restart
        AudioManager.Instance.GetComponent<AudioSource>().Stop();
        SceneManager.LoadScene("MainMenu");
    }
    public void RestartLevel()
    {
        soundSource.PlayOneShot(menuSelect);
        Time.timeScale = 1;
        insanity = 0; //Since insanity is a static variable, it needs to be reset at every restart
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}