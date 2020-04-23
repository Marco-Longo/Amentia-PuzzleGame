using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppearExtra : MonoBehaviour
{
    public Slider insanityMeter;

    void Update()
    {
        Color c = this.GetComponent<MeshRenderer>().material.color;
        c.a = Mathf.Clamp(Mathf.Pow((insanityMeter.value), 25), 0, 1);
        this.GetComponent<MeshRenderer>().material.color = c;
    }
}