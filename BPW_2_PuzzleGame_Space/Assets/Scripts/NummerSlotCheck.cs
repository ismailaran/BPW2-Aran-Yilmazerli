using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NummerSlotCheck : MonoBehaviour
{
    public GameObject nummer1;
    public GameObject nummer2;
    public GameObject nummer3;
    public UnityEvent interaction;
    bool executed = false;
    public bool HasPower;

    // Update is called once per frame
    void Update()
    {
        if (executed == false &&  HasPower)
        {
            if (nummer1.GetComponent<NummerslotRotate>().number == 6)
            {
                if (nummer2.GetComponent<NummerslotRotate>().number == 9)
                {
                    if (nummer3.GetComponent<NummerslotRotate>().number == 2)
                    {
                        executed = true;
                        interaction.Invoke();
                        
                    }
                }
            }
        }
    }

    public void powerOn()
    {
        HasPower = true;
    }

    public void PowerOff()
    {
        HasPower = false;
    }
}
