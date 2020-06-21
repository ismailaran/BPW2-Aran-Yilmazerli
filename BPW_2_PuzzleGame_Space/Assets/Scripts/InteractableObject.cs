using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{

    public UnityEvent interaction;
    public UnityEvent CrystalRemoved;
    public GameObject EnergySucker;
    public ParticleSystem Sparkles;

    void Update()
    {
        if (!EnergySucker.GetComponent<ForcePull>().IsPulling) CrystalRemoved.Invoke();
    }
    public void Interact()
    {
        if (EnergySucker.GetComponent<ForcePull>().IsPulling)
        {
            interaction.Invoke();
        } 
        else 
        {
            Sparkles.Play();
        }
    }
}
