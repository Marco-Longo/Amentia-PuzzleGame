using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;

    void Start()
    {
        //Hide the cursor during the game
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        //Press LShift to run (doubles the player's speed)
        if (Input.GetKeyDown(KeyCode.LeftShift))
            speed *= 2.0f;
        if (Input.GetKeyUp(KeyCode.LeftShift))
            speed /= 2.0f;

        //Calculate the new translation values for both the X and Z axes
        float translationZ = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float translationX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        //Apply the trasnformation to the player GameObject
        transform.Translate(translationX, 0, translationZ);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trapdoor"))
        {
            SceneManager.LoadScene("Second Floor");
        }
    }
}
