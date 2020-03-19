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
    public Slider insanityMeter;

    //Box Puzzle
    private int boxPuzzleCompletion = 0;
    private int boxesCount = 3;

    //Monster Insanity
    private float insanity = 0.0f;
    private float decayTimer = 0.0f;
    private bool inDanger = false;

    private void Awake()
    {
        if (!AudioManager.Instance.GetComponent<AudioSource>().isPlaying)
            AudioManager.Instance.GetComponent<AudioSource>().Play();
    }

    void Update()
    {
        decayTimer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Escape))
            OpenMenu();
        if (!inDanger && insanity > 0.0f && decayTimer > 3.0f)
            DecreaseInsanity();
    }

    //Box Puzzle Functions
    public void IncreasePuzzleCompletion()
    {
        boxPuzzleCompletion++;
        if (boxPuzzleCompletion == boxesCount) //when all 3 boxes are in position...
        {
            //... open the trapdoor
            trapdoorRot.GetComponent<Animator>().SetBool("Open", true);
            trapdoorRot.GetComponent<AudioSource>().Play();
            trapdoorTrigger.gameObject.SetActive(true);
        }
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
            inDanger = true;
            insanity += amount;
            decayTimer = 0.0f;
            insanityMeter.value = insanity;
        }
        if (insanity > 0.75f && !realDoor.activeInHierarchy)
        {
            realDoor.SetActive(true);
            Color c = realDoor.GetComponent<MeshRenderer>().material.color;
            c.a = 0.0f;
            realDoor.GetComponent<MeshRenderer>().material.color = c;
            realDoor.GetComponent<Animator>().SetBool("Insane", true);
            fakeWall.SetActive(false);
        }
    }
    private void DecreaseInsanity()
    {
        insanity -= 0.05f;
        decayTimer = 0.0f;
        insanityMeter.value = insanity;
    }
    public void InsanityDecay()
    {
        inDanger = false;
    }

    //Pause Menu Functions
    public void OpenMenu()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseMenu.gameObject.SetActive(true);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenu.gameObject.SetActive(false);
    }
    public void QuitGame()
    {
        Time.timeScale = 1;
        AudioManager.Instance.GetComponent<AudioSource>().Stop();
        SceneManager.LoadScene("MainMenu");
    }
    public void RestartLevel()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}