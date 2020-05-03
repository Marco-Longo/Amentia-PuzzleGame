using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public RectTransform mainMenu;
    public RectTransform controlsMenu;
    public RectTransform optionsMenu;
    public RectTransform creditsMenu;
    public Slider musicSlider;
    public Slider soundSlider;

    private AudioSource soundSource;
    public AudioSource musicSource;
    public AudioClip menuSelect;
    public AudioClip menuMovement;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        soundSource = GetComponent<AudioSource>();
        if (PlayerPrefs.GetInt("INDEX", -1) == -1)
            mainMenu.GetChild(1).GetComponent<Button>().interactable = false;
    }

    void Start()
    {
        //Check music settings
        if (PlayerPrefs.GetFloat("MUSIC", -1) == -1) //No music preferences
        {
            musicSlider.value = 1;
            musicSource.volume = 1;
            PlayerPrefs.SetFloat("MUSIC", 1.0f);
        }
        else
        {
            musicSlider.value = PlayerPrefs.GetFloat("MUSIC");
            musicSource.volume = musicSlider.value;
        }
        //Check sound settings
        if (PlayerPrefs.GetFloat("SOUND", -1) == -1) //No sound preferences
        {
            soundSlider.value = 1;
            soundSource.volume = 1;
            PlayerPrefs.SetFloat("SOUND", 1.0f);
        }
        else
        {
            soundSlider.value = PlayerPrefs.GetFloat("SOUND");
            soundSource.volume = soundSlider.value;
        }
        PlayerPrefs.Save();
    }

    public void ChangeMusicVolume()
    {
        float musicVolume = musicSlider.value;
        if (PlayerPrefs.GetFloat("MUSIC") != musicVolume)
        {
            PlayerPrefs.SetFloat("MUSIC", musicVolume);
            PlayerPrefs.Save();
            musicSource.volume = musicVolume;
        }
    }

    public void ChangeSoundVolume()
    {
        float soundVolume = soundSlider.value;
        if (PlayerPrefs.GetFloat("SOUND") != soundVolume)
        {
            PlayerPrefs.SetFloat("SOUND", soundVolume);
            PlayerPrefs.Save();
            soundSource.volume = soundVolume;
        }
    }

    //Load the first level of the game
    public void LoadGame()
    {
        soundSource.PlayOneShot(menuSelect);
        PlayerPrefs.SetInt("INDEX", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene("First Floor");
    }

    public void ContinueGame()
    {
        soundSource.PlayOneShot(menuSelect);
        int sceneIdx = PlayerPrefs.GetInt("INDEX", -1);

        if (sceneIdx > 0)
            SceneManager.LoadScene(sceneIdx);
        else
            SceneManager.LoadScene("First Floor");
    }

    //Load the controls tab in the main menu
    public void ShowControls()
    {
        soundSource.PlayOneShot(menuSelect);
        mainMenu.gameObject.SetActive(false);
        controlsMenu.gameObject.SetActive(true);
    }

    //Load the options tab in the main menu
    public void ShowOptions()
    {
        soundSource.PlayOneShot(menuSelect);
        mainMenu.gameObject.SetActive(false);
        optionsMenu.gameObject.SetActive(true);
    }
    
    //Load the credits tab in the main menu
    public void ShowCredits()
    {
        soundSource.PlayOneShot(menuSelect);
        mainMenu.gameObject.SetActive(false);
        creditsMenu.gameObject.SetActive(true);
    }

    //Load the main menu screen
    public void ShowMenu()
    {
        soundSource.PlayOneShot(menuMovement);
        controlsMenu.gameObject.SetActive(false);
        optionsMenu.gameObject.SetActive(false);
        creditsMenu.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
    }

    public void ExitGame()
    {
        soundSource.PlayOneShot(menuMovement);
#if !UNITY_EDITOR
        Application.Quit();
#endif
    }
}