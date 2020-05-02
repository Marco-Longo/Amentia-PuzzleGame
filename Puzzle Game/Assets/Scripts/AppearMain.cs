using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppearMain : MonoBehaviour
{
    public Slider insanityMeter;

    void Update()
    {
        Color c = this.GetComponent<MeshRenderer>().material.color;
        c.a = 1-( Mathf.Clamp(-100 * (Mathf.Pow(insanityMeter.value, 2.0f)) + (80f * insanityMeter.value) - (15f),0,1));
        this.GetComponent<MeshRenderer>().material.color = c;
    }


}


