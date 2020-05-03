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
    public Collider finalDoor;
    public GameObject endGameTrigger;
    public Slider insanityMeter;
    public Image insanityEffect;
    public GameObject deathScreen;
    private bool playerIsAlive = true;

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
    public AudioClip doorFadeSound;
    public AudioClip leverSound;
    public AudioClip gameOverSound;
    public AudioClip mainThemeSound;

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
        insanityEffect.color = new Color(insanityEffect.color.r, insanityEffect.color.g, insanityEffect.color.b, insanity);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && playerIsAlive)
            ToggleMenu();

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

    //Monster Insanity Functions (Second Level)
    public void IncreaseInsanity(float amount)
    {
        if (insanity < 1.0f)
        {  
            insanity += amount;
            insanityMeter.value = insanity;
            insanityEffect.color = new Color(insanityEffect.color.r, insanityEffect.color.g, insanityEffect.color.b, insanity);
        }
        if (insanity > 0.5f && !realDoor.activeInHierarchy)
        {
            fakeWall.GetComponent<Animator>().SetBool("Insane", true);
            soundSource.PlayOneShot(doorFadeSound);
            StartCoroutine(DoorFadeIn());
        }
        if (insanity >= 1.0f) //Death
            GameOver(); 
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

    //Cages Insanity Functions (Third Level)
    public void IncreaseInsanityCages()
    {
        StartCoroutine(SlowIncrease());
    }

    private IEnumerator SlowIncrease()
    {
        for (int i = 0; i < 20; i++)
        {
            insanity += 0.01f;
            insanityMeter.value = insanity;
            insanityEffect.color = new Color(insanityEffect.color.r, insanityEffect.color.g, insanityEffect.color.b, insanity);
            yield return new WaitForSeconds(.05f);
        }

        if (insanity > 0.38f && insanity < 0.42f) //Puzzle completed
        {
            finalDoor.enabled = (false);
            endGameTrigger.SetActive(true);
        }
        else
        {
            finalDoor.enabled = (true);
            endGameTrigger.SetActive(false);
        }

        if (insanity >= 1.0f) //Death
            GameOver();
    }

    public void ResetInsanity(GameObject flowers)
    {
        if (insanity <= 0)
            return;

        //Play flowers' sound effect
        flowers.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SOUND");
        flowers.GetComponent<AudioSource>().Play();
        //Flowers set insanity to 0 and close the door
        StartCoroutine(SlowReset());
        finalDoor.enabled = (true);
    }

    private IEnumerator SlowReset()
    {
        while (insanity > 0)
        {
            insanity -= 0.01f;
            insanityMeter.value = insanity;
            insanityEffect.color = new Color(insanityEffect.color.r, insanityEffect.color.g, insanityEffect.color.b, insanity);
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void GameOver()
    {
        soundSource.PlayOneShot(gameOverSound);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        deathScreen.SetActive(true);
        playerIsAlive = false;
    }

    public void DisableMenu()
    {
        playerIsAlive = false;
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
    public void ToggleMenu()
    {
        soundSource.PlayOneShot(menuMovement);
        if (pauseMenu.gameObject.activeInHierarchy)
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            pauseMenu.gameObject.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            pauseMenu.gameObject.SetActive(true);
        }
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
    public void QuitEndGame()
    {
        soundSource.PlayOneShot(menuMovement);
        Time.timeScale = 1;
        insanity = 0; //Since insanity is a static variable, it needs to be reset at every restart
        AudioManager.Instance.GetComponent<AudioSource>().clip = mainThemeSound;
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