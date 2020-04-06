using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private bool movementEnabled = true;
    private CharacterController controller;
    private GameObject gm;
    private AudioSource footstepsSFX;
    private GameObject lvr;
    private GameObject lvr2;
    private GameObject lvr3;

    public float speed = 10.0f;
    public float gravity = 20.0f;
    public float pushPower = 4.0f;
    public Image screenFade;

    void Start()
    {
        //Hide the cursor during the game
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        controller = GetComponent<CharacterController>();
        footstepsSFX = GetComponent<AudioSource>();
        gm = GameObject.Find("GameManager");
        lvr = GameObject.Find("Lever1");
        lvr2 = GameObject.Find("Lever2");
        lvr3 = GameObject.Find("Lever3");   
    }

    void Update()
    {
        //Press LShift to run (doubles the player's speed)
        if (Input.GetKeyDown(KeyCode.LeftShift))
            speed *= 2.0f;
        if (Input.GetKeyUp(KeyCode.LeftShift))
            speed /= 2.0f;

        if (movementEnabled)
        {
            // Get Horizontal and Vertical Input
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trapdoor")) //First floor cleared
        {
            //Save progress
            PlayerPrefs.SetInt("INDEX", 2);
            PlayerPrefs.Save();
            SceneManager.LoadScene("Second Floor");
        }
        else if (other.gameObject.CompareTag("FadeInDoor")) //Second floor cleared
        {
            //Save progress
            PlayerPrefs.SetInt("INDEX", 3);
            PlayerPrefs.Save();
            screenFade.GetComponent<ScreenFade>().FadeOut(3);
        }
        else if (other.gameObject.CompareTag("Cages")) //Level 3 insanity increase
        {
            gm.GetComponent<GameManager>().IncreaseInsanityCages(0.2f);
        }
        else if (other.gameObject.CompareTag("Flowers")) //Level 3 flowers insanity/colliders reset
        {
            gm.GetComponent<GameManager>().ResetInsanity();
            lvr.GetComponent<Lever>().Reset(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            gm.GetComponent<GameManager>().IncreaseInsanity(0.001f);
        }
    
        //When in the correct lever collider activate its effects on Cages
        if (other.gameObject.CompareTag("Controller1"))
        {
            lvr.GetComponent<Lever>().Controller1(true);
        }
        if (other.gameObject.CompareTag("Controller2"))
        {
            lvr2.GetComponent<Lever2>().Controller2(true);
        }
        if (other.gameObject.CompareTag("Controller3"))
        {
            lvr3.GetComponent<Lever3>().Controller3(true);
        }
    }

    /*
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            gm.GetComponent<GameManager>().InsanityDecay();
        }
    }
    */

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

    public Vector3 GetPosition()
    {
        return controller.transform.position;
    }
    public void SetPosition(Vector3 val)
    {
        controller.enabled = false;
        controller.transform.position = val;
        controller.enabled = true;
    }
    public void EnableMovement(bool move)
    {
        if (move == false)
            footstepsSFX.Stop();
        movementEnabled = move;
    }
}