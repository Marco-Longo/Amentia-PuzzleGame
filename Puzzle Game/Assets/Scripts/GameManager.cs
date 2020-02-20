using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public RectTransform pauseMenu;
    public GameObject trapdoorRot;
    public GameObject trapdoorTrigger;

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
            //... open the trapdoor
            trapdoorRot.GetComponent<Animator>().SetBool("Open", true);
            trapdoorTrigger.gameObject.SetActive(true);
        }
    }

    public void DecreasePuzzleCompletion()
    {
        boxPuzzleCompletion--;
        if (boxPuzzleCompletion != boxesCount)
        {
            Debug.Log("one or more boxes are not in place");
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