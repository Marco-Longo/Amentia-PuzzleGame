using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;

    void Start()
    {
        //Hide the cursor during the game
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //Press LShift to run (increases the player's speed)
        if (Input.GetKeyDown(KeyCode.LeftShift))
            speed += 10.0f;
        if (Input.GetKeyUp(KeyCode.LeftShift))
            speed -= 10.0f;

        //Calculate the new translation values for both the X and Z axes
        float translationZ = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float translationX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        //Apply the trasnformation to the player GameObject
        transform.Translate(translationX, 0, translationZ);

        //Press Esc to make the cursor reappear
        if (Input.GetKeyDown("escape"))
            Cursor.lockState = CursorLockMode.None;
    }
}
