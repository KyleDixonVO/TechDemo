using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour

{
    private Vector3 PosRDoor;
    private Vector3 PosLDoor;
    private Vector3 TranslateDirection = new Vector3(1f, 0, 0);
    private bool openDoor = false;
    public bool isAutoDoor = true;
    public int TranslateSpeed = 4;
    public GameObject leftDoor;
    public GameObject rightDoor;
    private InteractibleRaycast playerCast;
    private string doorPrompt = "E to open/close door";
    // Start is called before the first frame update
    void Start()
    {
        playerCast = GameObject.FindObjectOfType<InteractibleRaycast>();
        PosLDoor = leftDoor.transform.localPosition;
        PosRDoor = rightDoor.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        //buttonDoorPrompt();
        openCloseSequence();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") == true && isAutoDoor == true)
        {
            openDoor = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") == true && isAutoDoor == true)
        {
            openDoor = false;
        }
    }

    public string buttonDoorPrompt()
    {
        if (playerCast.withinRange == true && isAutoDoor == false)
        {
            return doorPrompt;
        }
        return null;
    }

    private void openCloseSequence()
    {
        if (openDoor == false)
        {
            if (rightDoor.transform.localPosition.x > PosRDoor.x)
            {
                rightDoor.transform.Translate(-TranslateDirection * Time.deltaTime * TranslateSpeed);
            }

            if (leftDoor.transform.localPosition.x < PosLDoor.x)
            {
                leftDoor.transform.Translate(TranslateDirection * Time.deltaTime * TranslateSpeed);
            }
        }
        else
        {
            if (rightDoor.transform.localPosition.x < PosRDoor.x + 1)
            {
                rightDoor.transform.Translate(TranslateDirection * Time.deltaTime * TranslateSpeed);
            }

            if (leftDoor.transform.localPosition.x > PosLDoor.x - 1)
            {
                leftDoor.transform.Translate(-TranslateDirection * Time.deltaTime * TranslateSpeed);
            }
        }
    }
}
