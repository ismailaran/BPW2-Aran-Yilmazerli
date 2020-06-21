using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesroyEngineRoomScript : MonoBehaviour
{
    public List<GameObject> engineRoomObjects;

    public void ExplodeEngineRoom()
    {
        foreach (GameObject roomObject in engineRoomObjects)
        {
            Destroy(roomObject);
        }
        Destroy(this.gameObject);
    }
}
