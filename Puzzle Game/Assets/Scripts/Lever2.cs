using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever2 : MonoBehaviour
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
    Animator lever2;

    private float LeverStart = 0f;
    private float LeverCooldown = 2.5f;

    void Start()
    {
        anim1 = CageOne.GetComponent<Animator>();
        anim2 = CageTwo.GetComponent<Animator>();
        anim3 = CageThree.GetComponent<Animator>();
        anim4 = CageFour.GetComponent<Animator>();
        anim5 = CageFive.GetComponent<Animator>();
        anim6 = CageSix.GetComponent<Animator>();
        lever2 = Handle.GetComponent<Animator>();
    }

    public void Controller2(bool amount)
    {
        if (Input.GetKeyDown("f"))
        {
            if (Time.time > LeverStart + LeverCooldown)
            {
                LeverStart = Time.time;

                ColliderTwo.enabled = !ColliderTwo.enabled;
                ColliderThree.enabled = !ColliderThree.enabled;
                ColliderFour.enabled = !ColliderFour.enabled;
                ColliderFive.enabled = !ColliderFive.enabled;
                ColliderSix.enabled = !ColliderSix.enabled;

                if (lever2.GetBool("Leveron") == true)
                {
                    lever2.SetBool("Leveron", false);
                }
                else
                {
                    lever2.SetBool("Leveron", true);
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
                if (!ColliderFive.enabled)
                {
                    anim5.SetBool("Rising5", true);
                }
                if (ColliderFive.enabled)
                {
                    anim5.SetBool("Rising5", false);
                }
                if (!ColliderSix.enabled)
                {
                    anim6.SetBool("Rising6", true);
                }
                if (ColliderSix.enabled)
                {
                    anim6.SetBool("Rising6", false);
                }
            }
        }
    }
}
