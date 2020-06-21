using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotation : MonoBehaviour
{
    public float rotatespeed;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotatespeed * Time.deltaTime, 0);
    }
}
