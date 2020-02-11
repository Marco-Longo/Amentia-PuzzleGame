using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    //public AudioClip buttonClick;
    //private AudioSource audioSource;
    public RectTransform mainMenu;
    public RectTransform controlsMenu;
    public RectTransform optionsMenu;

    private void Awake()
    {
        Cursor.visible = true;
    }

    void Start()
    {
        //audioSource = gameObject.GetComponent<AudioSource>();
    }

    //Load the first level of the game
    public void LoadGame()
    {
        //audioSource.PlayOneShot(buttonClick);
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
