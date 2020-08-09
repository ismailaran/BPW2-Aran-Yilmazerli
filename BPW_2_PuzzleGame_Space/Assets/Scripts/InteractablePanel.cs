using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractablePanel : MonoBehaviour
{

    public UnityEvent interaction;
    public UnityEvent SecondInteraction;
    public int interactionCount = 0;
    public bool isAntennaConnected = false;


    public void Interact()
    {
        if (interactionCount == 0)
        {
            interactionCount++;
            interaction.Invoke();
        }
        if (interactionCount == 1 && isAntennaConnected) SecondInteraction.Invoke();
    }

    public void resetCount()
    {
        interactionCount = 0;
    }

    public void antennaConnected()
    {
        isAntennaConnected = true;
    }
}
