using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NummerslotRotate : MonoBehaviour
{
    public int number;
public void rotateNumber()
    {
        Quaternion RotateVar = transform.rotation * Quaternion.Euler(0, 0, -36);
        transform.rotation = RotateVar;
        number++;
    }
}
