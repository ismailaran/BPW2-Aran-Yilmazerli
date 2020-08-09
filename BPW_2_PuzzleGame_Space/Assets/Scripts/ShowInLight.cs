using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInLight : MonoBehaviour
{
    public GameObject player;

    void Start()
    {
        gameObject.GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<Light>().enabled == true)
        {
            gameObject.GetComponent<Renderer>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<Renderer>().enabled = false;
        }
    }
}
