using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int boxPuzzleCompletion = 0;
    private int boxesCount = 3;
 
    void Update()
    {
        
    }

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
}


