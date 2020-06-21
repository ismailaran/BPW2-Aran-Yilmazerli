using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    public Image fadepanel;
    public float fadeSpeed, SecondsToFade;
    float fadeLevel = 1f;
    bool fadeIn;


    void Start()
    {
        Color fade = fadepanel.color;
        fade.a = 1f;
        fadepanel.color = fade;
    }

    void Update()
    {
        if (!fadeIn)
        {
            SecondsToFade -= Time.deltaTime;
            if (SecondsToFade <= 0) fadeIn = true;

        }

        if (fadeIn)
        {
            fadeLevel -= Time.deltaTime * fadeSpeed;
            Color fade = fadepanel.color;
            fade.a = fadeLevel;
            fadepanel.color = fade;
        }
        if (fadeLevel <= 0)
        {
            PlayerMovement.canRotate = true;
            Destroy(this);
        }
    }
}
