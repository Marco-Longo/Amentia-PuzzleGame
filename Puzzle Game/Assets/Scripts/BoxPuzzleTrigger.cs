using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPuzzleTrigger : MonoBehaviour
{
    public GameObject gm;

    void OnTriggerEnter(Collider other)
    {
        //When the box enters the correct space (measured by a trigger box) increase puzzle completion
        if (other.isTrigger == true && other.CompareTag("boxPuzzleTrigger"))
        {
            gm.GetComponent<GameManager>().IncreasePuzzleCompletion();   
        }
    }

    void OnTriggerExit(Collider other)
    {
        //When the box is no longer in position, decrease puzzle completion
        if (other.isTrigger == true && other.CompareTag("boxPuzzleTrigger"))
        {
            gm.GetComponent<GameManager>().DecreasePuzzleCompletion();
        }
    }
}



