using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public RectTransform pauseMenu;

    private int boxPuzzleCompletion = 0;
    private int boxesCount = 3;
 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            OpenMenu();
    }

    //Box Puzzle Functions
    public void IncreasePuzzleCompletion()
    {
        boxPuzzleCompletion++;
        if (boxPuzzleCompletion == boxesCount) //when all 3 boxes are in position...
        {
            Debug.Log("puzzle completed!");
            //this can trigger a door opening...
        }
    }

    public void DecreasePuzzleCompletion()
    {
        boxPuzzleCompletion--;
        if (boxPuzzleCompletion != boxesCount) //when all 3 boxes are in position...
        {
            Debug.Log("one or more boxes are not in place");
            //this can trigger a door opening...
        }
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