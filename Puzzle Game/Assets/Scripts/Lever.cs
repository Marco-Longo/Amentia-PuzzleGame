using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public GameObject CageOne;
    public GameObject CageTwo;
    public GameObject CageThree;
    public GameObject CageFour;
    public GameObject CageFive;
    public GameObject CageSix;
    public GameObject Handle;

    public Collider ColliderOne;
    public Collider ColliderTwo;
    public Collider ColliderThree;
    public Collider ColliderFour;
    public Collider ColliderFive;
    public Collider ColliderSix;

    Animator anim1;
    Animator anim2;
    Animator anim3;
    Animator anim4;
    Animator anim5;
    Animator anim6;
    Animator lever1;

    private float LeverStart = -1f;
    private float LeverCooldown = 2.5f;
    private bool NearLever;

    void Start()
    {
        anim1 = CageOne.GetComponent<Animator>();
        anim2 = CageTwo.GetComponent<Animator>();
        anim3 = CageThree.GetComponent<Animator>();
        anim4 = CageFour.GetComponent<Animator>();
        anim5 = CageFive.GetComponent<Animator>();
        anim6 = CageSix.GetComponent<Animator>();
        lever1 = Handle.GetComponent<Animator>();
    }

    // After being in flowers checks if the cage is down and reactives the correct colliders
    public void Reset(bool amount)
    {
        if ((anim1.GetBool("Rising1") == false))
        {
            ColliderOne.enabled = true;
        }
        if ((anim2.GetBool("Rising2") == false))
        {
            ColliderTwo.enabled = true;
        }
        if ((anim3.GetBool("Rising3") == false))
        {
            ColliderThree.enabled = true;
        }
        if ((anim4.GetBool("Rising4") == false))
        {
            ColliderFour.enabled = true;
        }
        if ((anim5.GetBool("Rising5") == false))
        {
            ColliderFive.enabled = true;
        }
        if ((anim6.GetBool("Rising6") == false))
        {
            ColliderSix.enabled = true;
        }
    }

    //When F is pressed flip the cages/levers from down to up or vice-versa
    public void Controller1(bool amount)
    {
        NearLever = amount;
    }

    private void Update()
    {
        if (Input.GetKeyDown("f") && (NearLever == true))
        {
            if (Time.time > LeverStart + LeverCooldown) //add a cooldown to avoid button spamming issues
            {
                LeverStart = Time.time;
                GameObject.Find("GameManager").GetComponent<GameManager>().PlayLeverSound();

                ColliderTwo.enabled = !ColliderTwo.enabled;
                ColliderThree.enabled = !ColliderThree.enabled;
                ColliderFour.enabled = !ColliderFour.enabled;

                if (lever1.GetBool("Leveron") == true) //flip lever animation
                {
                    lever1.SetBool("Leveron", false);
                }
                else
                {
                    lever1.SetBool("Leveron", true);
                }

                if (!ColliderTwo.enabled)
                {
                    anim2.SetBool("Rising2", true);
                }
                if (ColliderTwo.enabled)
                {
                    anim2.SetBool("Rising2", false);
                }
                if (!ColliderThree.enabled)
                {
                    anim3.SetBool("Rising3", true);
                }
                if (ColliderThree.enabled)
                {
                    anim3.SetBool("Rising3", false);
                }
                if (!ColliderFour.enabled)
                {
                    anim4.SetBool("Rising4", true);
                }
                if (ColliderFour.enabled)
                {
                    anim4.SetBool("Rising4", false);
                }
            }
        }
    }
}