using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcePull : MonoBehaviour
{
    public bool IsPulling = false;
    GameObject PullObject;
    public float PullStrength;
    public GameObject EnergyRock;

    void Start()
    {
        if( IsPulling == true)
        {
            PullObject = Instantiate(EnergyRock, transform.position, transform.rotation);
            PullObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (IsPulling == false)
        {
            PullObject = other.gameObject;
            IsPulling = true;
            StartCoroutine (Pull(PullObject.transform, transform));
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == PullObject)
        {
            PullObject = null;
            IsPulling = false;
        }
    }

    IEnumerator Pull(Transform StartPos, Transform Target)
    {
        float LerpDistance = 0f;

        while (Vector3.Distance(PullObject.transform.position, Target.position) > 0.05f)
        {
            LerpDistance = LerpDistance + PullStrength * Time.deltaTime;
            PullObject.transform.position = Vector3.Lerp(StartPos.position, Target.position, LerpDistance);
            PullObject.transform.rotation = Quaternion.Lerp(StartPos.transform.rotation , Target.transform.rotation, LerpDistance);
            Debug.Log(LerpDistance);
            yield return null;
        }
        PullObject.GetComponent<Rigidbody>().isKinematic = true;
        yield return new WaitForSeconds(3f);
    }
}
