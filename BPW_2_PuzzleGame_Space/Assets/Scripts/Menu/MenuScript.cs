using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuScript : MonoBehaviour
{
    public Image startNormal;
    public Image startHover;
    public Image quitNormal;
    public Image quitHover;
    int selectedOption = 0;
    bool buttonPressed = false;

    void Start()
    {
        startHover.enabled = false;
        quitHover.enabled = false;
    }

    void Update()
    {
        if (selectedOption == 0)
        {
            startHover.enabled = true;

            quitHover.enabled = false;
        }

        if (selectedOption == 1)
        {
            startHover.enabled = false;

            quitHover.enabled = true;
        }

        if (Input.GetAxis("Vertical") == 0) buttonPressed = false;
        if (Input.GetAxis("Vertical") < 0)
        {
            if (selectedOption <1) selectedOption++;
            buttonPressed = true;
        }
        if (Input.GetAxis("Vertical") > 0)
        {
            if (selectedOption > 0) selectedOption--;
            buttonPressed = true;
        } 

        if (Input.GetButtonDown("Interact"))
        {
            if (selectedOption == 0) SceneManager.LoadScene("GameScene");
            if (selectedOption == 1) Application.Quit();
            Debug.Log("interact pressed");
        }
    }

    public void hoverOverStart()
    {
        selectedOption = 0;
    }

    public void hoverOverQuit()
    {
        selectedOption = 1;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
