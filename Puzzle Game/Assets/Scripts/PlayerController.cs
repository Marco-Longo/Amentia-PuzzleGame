using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private GameObject gm;
    private AudioSource footstepsSFX;

    public float speed = 10.0f;
    public float gravity = 20.0f;
    public float pushPower = 4.0f;

    void Start()
    {
        //Hide the cursor during the game
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        controller = GetComponent<CharacterController>();
        gm = GameObject.Find("GameManager");
        footstepsSFX = GetComponent<AudioSource>();
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

        //Play Footsteps SFX
        if (controller.velocity.magnitude > 2.0f && !footstepsSFX.isPlaying)
        {
            footstepsSFX.volume = Random.Range(0.8f, 1.0f);
            footstepsSFX.pitch = Random.Range(0.8f, 1.1f);
            footstepsSFX.Play();
        }
        else if (controller.velocity.magnitude < 2.0f && footstepsSFX.isPlaying)
            footstepsSFX.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trapdoor"))
        {
            SceneManager.LoadScene("Second Floor");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            gm.GetComponent<GameManager>().IncreaseInsanity(0.001f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            gm.GetComponent<GameManager>().InsanityDecay();
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

        //From Character to Box
        Vector3 direction = (other.gameObject.transform.position - transform.position).normalized;
        Ray ray = new Ray(transform.position, direction);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        Vector3 normal = hit.normal;
        normal.y = 0.0f;

        //From Box to Character
        Vector3 direction2 = (transform.position - other.gameObject.transform.position).normalized;
        Ray ray2 = new Ray(other.gameObject.transform.position, direction2);
        RaycastHit hit2;
        Physics.Raycast(ray2, out hit2);
        Vector3 normal2 = hit2.normal;
        normal2.y = 0.0f;

        // Apply the push if character is facing forward
        if ((Mathf.Abs(normal2.x - transform.forward.x) < 0.2f) && (Mathf.Abs(normal2.z - transform.forward.z) < 0.2f))
        {
            //Assist Player Movement
            //...

            body.velocity = pushPower * -normal;
        }
    }
}
