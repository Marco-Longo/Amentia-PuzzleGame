using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppearExtra : MonoBehaviour
{
    public Slider insanityMeter;

    void Update()
    {
        //Door
        Color c = this.GetComponent<MeshRenderer>().material.color;
        c.a = Mathf.Clamp(Mathf.Pow((insanityMeter.value), 25), 0, 1);
        this.GetComponent<MeshRenderer>().material.color = c;
        //Sigil
        Color c1 = this.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material.color;
        c1.a = Mathf.Clamp(Mathf.Pow((insanityMeter.value), 25), 0, 1);
        this.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material.color = c1;
    }
}