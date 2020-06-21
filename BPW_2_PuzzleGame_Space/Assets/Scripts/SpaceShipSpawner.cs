using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipSpawner : MonoBehaviour
{
    public GameObject spaceship;

    public void spawnShip()
    {
        spaceship.SetActive(true);
    }
}
