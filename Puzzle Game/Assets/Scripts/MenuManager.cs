using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    //public AudioClip buttonClick;
    public AudioSource audioSource;
    public RectTransform mainMenu;
    public RectTransform controlsMenu;
    public RectTransform optionsMenu;
    public Slider musicSlider;
    public Slider soundSlider;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (PlayerPrefs.GetInt("INDEX", -1) == -1)
            mainMenu.GetChild(1).GetComponent<Button>().interactable = false;
    }

    void Start()
    {
        if (PlayerPrefs.GetFloat("MUSIC", -1) == -1) //No music preferences
        {
            musicSlider.value = 1;
            audioSource.volume = 1;
            PlayerPrefs.SetFloat("MUSIC", 1.0f);
        }
        else
        {
            musicSlider.value = PlayerPrefs.GetFloat("MUSIC");
            audioSource.volume = musicSlider.value;
        }

        if (PlayerPrefs.GetFloat("SOUND", -1) == -1) //No sound preferences
        {
            soundSlider.value = 1;
            PlayerPrefs.SetFloat("SOUND", 1.0f);
        }
        else
        {
            soundSlider.value = PlayerPrefs.GetFloat("SOUND");
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
            audioSource.volume = musicVolume;
        }
    }

    public void ChangeSoundVolume()
    {
        float soundVolume = soundSlider.value;
        if (PlayerPrefs.GetFloat("SOUND") != soundVolume)
        {
            PlayerPrefs.SetFloat("SOUND", soundVolume);
            PlayerPrefs.Save();
        }
    }

    //Load the first level of the game
    public void LoadGame()
    {
        //audioSource.PlayOneShot(buttonClick);
        PlayerPrefs.SetInt("INDEX", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene("First Floor");
    }

    public void ContinueGame()
    {
        //audioSource.PlayOneShot(buttonClick);
        int sceneIdx = PlayerPrefs.GetInt("INDEX", -1);

        if (sceneIdx > 0)
            SceneManager.LoadScene(sceneIdx);
        else
            SceneManager.LoadScene("First Floor");
    }

    //Load the controls tab in the main menu
    public void ShowControls()
    {
        //audioSource.PlayOneShot(buttonClick);
        mainMenu.gameObject.SetActive(false);
        controlsMenu.gameObject.SetActive(true);
    }

    //Load the options tab in the main menu
    public void ShowOptions()
    {
        //audioSource.PlayOneShot(buttonClick);
        mainMenu.gameObject.SetActive(false);
        optionsMenu.gameObject.SetActive(true);
    }

    //Load the main menu screen
    public void ShowMenu()
    {
        //audioSource.PlayOneShot(buttonClick);
        controlsMenu.gameObject.SetActive(false);
        optionsMenu.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
    }

    public void ExitGame()
    {
        //audioSource.PlayOneShot(buttonClick);
#if !UNITY_EDITOR
        Application.Quit();
#endif
    }
}
