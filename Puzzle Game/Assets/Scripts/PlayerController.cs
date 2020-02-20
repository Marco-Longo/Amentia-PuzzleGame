using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;

    public float speed = 10.0f;
    public float gravity = 20.0f;
    public float pushPower = 4.0f;

    void Start()
    {
        //Hide the cursor during the game
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        //Press LShift to run (doubles the player's speed)
        if (Input.GetKeyDown(KeyCode.LeftShift))
            speed *= 2.0f;
        if (Input.GetKeyUp(KeyCode.LeftShift))
            speed /= 2.0f;

        // Get Horizontal and Vertical Input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the Direction to Move based on the tranform of the Player
        Vector3 moveDirectionForward = transform.forward * verticalInput;
        Vector3 moveDirectionSide = transform.right * horizontalInput;

        //find the direction
        Vector3 direction = (moveDirectionForward + moveDirectionSide).normalized;
        //find the distance
        Vector3 distance = direction * speed * Time.deltaTime;
        distance.y -= gravity * Time.deltaTime;

        // Apply Movement to Player
        controller.Move(distance);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trapdoor"))
        {
            SceneManager.LoadScene("Second Floor");
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit other)
    {
        Rigidbody body = other.collider.attachedRigidbody;
        
        // No rigidbody
        if (body == null || body.isKinematic) 
            return;
        // We dont want to push objects below us
        if (other.moveDirection.y < -0.3f)
            return;

        Vector3 direction = (other.gameObject.transform.position - transform.position).normalized;
        Ray ray = new Ray(transform.position, direction);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        Vector3 normal = hit.normal;
        normal.y = 0.0f;

        // Apply the push
        body.velocity = pushPower * -normal;
    }
}
