using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScreenFade : MonoBehaviour
{
    public GameObject player;

    private void Awake()
    {
        if (GetComponent<Image>().color.a == 1)
            GetComponent<Animator>().SetBool("FadeIn", true);
    }

    public void FadeOut(int idx)
    {
        StartCoroutine(ScreenFadeOut(idx));
    }

    private IEnumerator ScreenFadeOut(int idx)
    {
        GetComponent<Animator>().SetBool("FadeOut", true);
        player.GetComponent<PlayerController>().EnableMovement(false);
        
        yield return new WaitUntil(() => GetComponent<Image>().color.a == 1);
        SceneManager.LoadScene(idx);
    }
}
