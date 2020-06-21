using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePanelScreen : MonoBehaviour
{

    public Material firstNewScreen;
    public Material SecondNewScreen;
    public Material ThirdScreen;
    public Material FourthScreen;
    float timeToSwitch = 2f;
    bool CountingDown = false;
    
    void Update()
    {
        if (CountingDown)
        {
            timeToSwitch -= Time.deltaTime;
            if (timeToSwitch <= 0) GetComponent<Renderer>().material = FourthScreen;
        }
    }

    public void changeScreen()
    {
        GetComponent<Renderer>().material = firstNewScreen;
    }

    public void ChangeToConnected()
    {
        GetComponent<Renderer>().material = SecondNewScreen;
    }

    public void changeToSend()
    {
        GetComponent<Renderer>().material = ThirdScreen;
        CountingDown = true;
    }
}
