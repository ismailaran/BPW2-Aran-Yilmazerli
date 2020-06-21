using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public static bool canRotate = false;
    public float thrustSpeed, torqueSpeed, maxRotateSpeed, grabSpeed, throwSpeed;
    public Image InteractPrompt, GrabPrompt, ThrowPrompt, SlowPrompt, CoordinatesPrompt;
    bool R3Pressed = false;
    Vector3 rbRotationControl;
    Rigidbody rb;
    Light flashlight;
    private RaycastHit hit;
    //int layerMask = 1 << 8;
    public LayerMask layerMask;
    GameObject GrabbedObject;
    public GameObject GrabLocation;
    bool objectGrabbed, jetpackGrabbed = false;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false; 
        rb = GetComponent<Rigidbody>();
        flashlight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Physics.Raycast(rb.transform.position, rb.transform.forward, out hit, 5))
        {
            Debug.Log(hit.collider.gameObject.name);
        }
        

        // disable ui prompts
        UIPrompts();


        // Add force to player for movement, but not when pressing slowdown button
        if (Input.GetAxis("SlowDown") == 0 && jetpackGrabbed) rb.AddRelativeForce(Input.GetAxis("Horizontal") * thrustSpeed, ((Input.GetAxis("Thrust Up") + 1) - (Input.GetAxis("Thrust Down") + 1)) * thrustSpeed, Input.GetAxis("Vertical") * thrustSpeed);

        //player rotation control
        if (canRotate)
        {
            if (Input.GetAxis("Roll Control") != 0)
            {
                rbRotationControl = new Vector3(0, 0, -1 * Mathf.Clamp(Input.GetAxis("CameraY") * 100, -1 * maxRotateSpeed, maxRotateSpeed));
            }
            else
            {
                rbRotationControl = new Vector3(Mathf.Clamp(Input.GetAxis("CameraX") * 100, -1 * maxRotateSpeed, maxRotateSpeed), Mathf.Clamp(Input.GetAxis("CameraY") * 100, -1 * maxRotateSpeed, maxRotateSpeed), 0);
            }
            Quaternion deltaRotation = Quaternion.Euler(rbRotationControl * Time.deltaTime);
            rb.MoveRotation(rb.rotation * deltaRotation);
        }

        //press R3 on controller or E on keyboard to activate/deactivate light
        if (R3Pressed == false && Input.GetAxis("FlashLight") != 0) { flashlight.enabled = !flashlight.enabled; R3Pressed = true; }
        if (Input.GetAxis("FlashLight") == 0) R3Pressed = false;

        //apply reversed force to slowdown/stop player
        if (Input.GetAxis("SlowDown") != 0)
        {
            if (rb.velocity.magnitude >= thrustSpeed)
            {
                rb.AddForce(Vector3.Normalize(rb.velocity) * -1 * thrustSpeed);
            }
            else if (rb.velocity.magnitude >= 0.05)
            {
                rb.AddForce(rb.velocity * -1);
            }
            else 
            { 
                rb.velocity = Vector3.zero;
            }
        }

        // raycast check for interactable objects and activate interact script
        Debug.DrawRay(rb.transform.position, rb.transform.forward + (rb.transform.forward * 5), Color.yellow);
        if  (Physics.Raycast(rb.transform.position, rb.transform.forward, out hit, 5, layerMask))
        {

            if (hit.collider.tag == "Interactable")
            {
                //checks which prompt needs to be enabled between the "interact" and "send coordinates" prompts
                InteractPrompt.GetComponent<Image>().enabled = true;
                if (hit.collider.gameObject.GetComponent<InteractablePanel>() != null)
                {
                    if (hit.collider.gameObject.GetComponent<InteractablePanel>().interactionCount != 0) InteractPrompt.GetComponent<Image>().enabled = false;
                    if (hit.collider.gameObject.GetComponent<InteractablePanel>().interactionCount == 1 && hit.collider.gameObject.GetComponent<InteractablePanel>().isAntennaConnected) CoordinatesPrompt.GetComponent<Image>().enabled = true;
                }
                if (Input.GetButtonDown("Interact"))
                {
                    if (hit.collider.gameObject.GetComponent<InteractableObject>() != null)hit.collider.gameObject.GetComponent<InteractableObject>().Interact();
                    if (hit.collider.gameObject.GetComponent<InteractableEnginePanel>() != null) hit.collider.gameObject.GetComponent<InteractableEnginePanel>().Interact();
                    if (hit.collider.gameObject.GetComponent<InteractablePanel>() != null) hit.collider.gameObject.GetComponent<InteractablePanel>().Interact();
                }
            }

            if (hit.collider.tag == "Crystal")
            {
                if (!objectGrabbed)
                {
                    GrabPrompt.GetComponent<Image>().enabled = true;
                    if (Input.GetButtonDown("Interact"))
                    {
                        GrabbedObject = hit.collider.gameObject;
                        StartCoroutine(GrabbingAnimation(hit.collider.gameObject.transform));
                    }
                }    
            }
            if (hit.collider.tag == "jetpack")
            {
                GrabPrompt.GetComponent<Image>().enabled = true;
                if (Input.GetButtonDown("Interact"))
                {
                    jetpackGrabbed = true;
                    Destroy(hit.collider.gameObject);
                }
            }
        }

        if (objectGrabbed == true)
        {
            GrabbedObject.transform.position = GrabLocation.transform.position;
            GrabbedObject.transform.rotation = GrabLocation.transform.rotation;

            if (Input.GetButtonDown("Throw"))
            {
                GrabbedObject.GetComponent<Rigidbody>().isKinematic = false;
                GrabbedObject.GetComponent<Rigidbody>().AddForce(rb.transform.forward * throwSpeed, ForceMode.Impulse);
                GrabbedObject.GetComponent<MeshCollider>().enabled = true;
                objectGrabbed = false;
            }
        }

        
    }



    void UIPrompts()
    {
        //enable/disable slowdown button prompt when moving/not moving
        SlowPrompt.GetComponent<Image>().enabled = false;
        if (rb.velocity.magnitude >= 0.01f) SlowPrompt.GetComponent<Image>().enabled = true;

        //enable/disable throw button prompt if object held or not
        ThrowPrompt.GetComponent<Image>().enabled = false;
        if (objectGrabbed) ThrowPrompt.GetComponent<Image>().enabled = true;

        //disable button prompts when not necessary
        InteractPrompt.GetComponent<Image>().enabled = false;
        CoordinatesPrompt.GetComponent<Image>().enabled = false;
        GrabPrompt.GetComponent<Image>().enabled = false;
    }

    IEnumerator GrabbingAnimation(Transform StartPos)
    {
        float LerpDistance = 0f;

        while (Vector3.Distance(GrabbedObject.transform.position, GrabLocation.transform.position) > 0.05f)
        {
            LerpDistance = LerpDistance + grabSpeed * Time.deltaTime;
            GrabbedObject.transform.position = Vector3.Lerp(StartPos.position, GrabLocation.transform.position, LerpDistance);
            GrabbedObject.transform.rotation = Quaternion.Lerp(StartPos.transform.rotation, GrabLocation.transform.rotation, LerpDistance);
            yield return null;
        }
        GrabbedObject.GetComponent<Rigidbody>().isKinematic = true;
        GrabbedObject.GetComponent<MeshCollider>().enabled = false;
        objectGrabbed = true;
        yield return new WaitForSeconds(3f);
    }
}
