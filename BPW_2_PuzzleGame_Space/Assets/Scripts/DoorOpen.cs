using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{

    public bool Open;
    bool mustClose = false;
    bool moving = false;
    Vector3 OpenLoc, CloseLoc;
    float DoorSpeed = 1.6f;
    // Start is called before the first frame update
    void Start()
    {
        if (Open)
        {
            OpenLoc = transform.position;
            CloseLoc = new Vector3(OpenLoc.x, OpenLoc.y - 2, OpenLoc.z);
        } else if (!Open)
        {
            CloseLoc = transform.position;
            OpenLoc = new Vector3(CloseLoc.x, CloseLoc.y + 2, CloseLoc.z);
        }
    }

    public void OpenClose()
    {
        if (!moving)
        {
            if (Open)
            {
                StartCoroutine(Move(OpenLoc, CloseLoc));
            }
            else
                StartCoroutine(Move(CloseLoc, OpenLoc));
        }
    }

    public void ForceClose()
    {
        if (Open)
        {
            if (moving) mustClose = true;
            if (!moving) StartCoroutine(ForceClose(OpenLoc, CloseLoc));
        }
    }

    IEnumerator Move(Vector3 StartPos, Vector3 Target)
    {
        moving = true;
        float LerpDistance = 0f;
        while (Vector3.Distance(transform.position, Target) > 0.05f)
        {
            LerpDistance = LerpDistance + DoorSpeed * Time.deltaTime;
            transform.position = Vector3.Lerp(StartPos, Target, LerpDistance);

            yield return null;
        }
        if (mustClose) StartCoroutine(ForceClose(OpenLoc, CloseLoc));
        moving = false;
        Open = !Open;
        yield return new WaitForSeconds(3f);
    }

    IEnumerator ForceClose(Vector3 StartPos, Vector3 Target)
    {
        float LerpDistance = 0f;
        while (Vector3.Distance(transform.position, Target) > 0.05f)
        {
            LerpDistance = LerpDistance + DoorSpeed * Time.deltaTime;
            transform.position = Vector3.Lerp(StartPos, Target, LerpDistance);

            yield return null;
        }
        yield return new WaitForSeconds(3f);
    }
}
