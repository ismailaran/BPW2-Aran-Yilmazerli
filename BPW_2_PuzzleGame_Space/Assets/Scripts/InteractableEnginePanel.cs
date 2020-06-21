using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableEnginePanel : MonoBehaviour
{
    public UnityEvent interaction;
    public UnityEvent CrystalRemoved;
    public List<GameObject> EnergySucker;
    public ParticleSystem Sparkles;

    void Update()
    {
        foreach (GameObject energySucker in EnergySucker)
        {
            if (!energySucker.GetComponent<ForcePull>().IsPulling) CrystalRemoved.Invoke();
        }
    }
    public void Interact()
    {
        bool canInteract = true;
        foreach (GameObject energySucker in EnergySucker)
        {
            if (!energySucker.GetComponent<ForcePull>().IsPulling)
            {
                canInteract = false;
            }
        }
        if (canInteract)
        {
            interaction.Invoke();
        }
        else
        {
            Debug.Log("engine error");
        }
    }
}

