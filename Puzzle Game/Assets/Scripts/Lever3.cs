using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever3 : MonoBehaviour
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

    public GameObject interactionGUI;

    Animator anim1;
    Animator anim2;
    Animator anim3;
    Animator anim4;
    Animator anim5;
    Animator anim6;
    Animator lever3;

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
        lever3 = Handle.GetComponent<Animator>();
    }

    public void Controller3(bool amount)
    {
        NearLever = amount;
        interactionGUI.SetActive(amount);
    }

    private void Update()
    {
        if (Input.GetKeyDown("f") && (NearLever == true) && Time.timeScale != 0)
        {
            if (Time.time > LeverStart + LeverCooldown)
            {
                LeverStart = Time.time;
                GameObject.Find("GameManager").GetComponent<GameManager>().PlayLeverSound();

                if (lever3.GetBool("Leveron") == true)
                {
                    lever3.SetBool("Leveron", false);
                }
                else
                {
                    lever3.SetBool("Leveron", true);
                }

                ColliderThree.enabled = !ColliderThree.enabled;
                ColliderFive.enabled = !ColliderFive.enabled;
                ColliderSix.enabled = !ColliderSix.enabled;

                if (!ColliderThree.enabled)
                {
                    anim3.SetBool("Rising3", true);
                    CageThree.transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(false);
                }
                if (ColliderThree.enabled)
                {
                    anim3.SetBool("Rising3", false);
                    CageThree.transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(true);
                }
                if (!ColliderFive.enabled)
                {
                    anim5.SetBool("Rising5", true);
                    CageFive.transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(false);
                }
                if (ColliderFive.enabled)
                {
                    anim5.SetBool("Rising5", false);
                    CageFive.transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(true);
                }
                if (!ColliderSix.enabled)
                {
                    anim6.SetBool("Rising6", true);
                    CageSix.transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(false);
                }
                if (ColliderSix.enabled)
                {
                    anim6.SetBool("Rising6", false);
                    CageSix.transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(true);
                }
            }
        }
    }
}