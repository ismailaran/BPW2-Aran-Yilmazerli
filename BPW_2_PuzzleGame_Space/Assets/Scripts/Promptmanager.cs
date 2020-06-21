using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Promptmanager : MonoBehaviour
{
    float lookAround = 2f;
    public Image lookAroundPrompt;
    // Update is called once per frame
    void Update()
    {
        if ((Input.GetAxis("CameraX") != 0) || Input.GetAxis("CameraY") != 0)
        {
            lookAround -= Time.deltaTime;
            if (lookAround <= 0) lookAroundPrompt.enabled = false;
        }
    }
}
