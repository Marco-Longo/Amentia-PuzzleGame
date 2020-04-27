using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsController : MonoBehaviour
{
    private static ArmsController instance = null;
    private Animator anim;

    public static ArmsController Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
            instance = this;
    }

    private void Start()
    {
        anim = GetComponent<Animator>();   
    }

    public void ToggleRun(bool running)
    {
        anim.SetBool("Run", running);
    }

    public void ToggleBoxPush(bool push)
    {
        anim.SetBool("Box", push);
    }
    
    public void ToggleDoorPush(bool push)
    {
        anim.SetBool("Door", push);
    }
}
