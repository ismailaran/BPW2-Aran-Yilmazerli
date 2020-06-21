using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public bool LightOn;
    public Material OnMat;
    public Material OffMat;
    [SerializeField]
    private GameObject[] lights;

    public void OnOff()
    {
        foreach (GameObject light in lights)
        {
            light.GetComponent<Light>().enabled = !LightOn;
        }
        if (LightOn)
        {
            GetComponent<Renderer>().material = OffMat;
        }
        else if (!LightOn)
        {
            GetComponent<Renderer>().material = OnMat;
        }
        LightOn = !LightOn;
    }

    public void ForceOff()
    {

            foreach (GameObject light in lights)
            {
                light.GetComponent<Light>().enabled = false;
            }
            GetComponent<Renderer>().material = OffMat;
            
    }
}
