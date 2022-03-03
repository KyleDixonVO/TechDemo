using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Door : MonoBehaviour

{
    private Vector3 PosRDoor;
    private Vector3 PosLDoor;
    private Vector3 TranslateDirection = new Vector3(1f, 0, 0);
    private bool openDoor = false;
    public bool isAutoDoor = true;
    private bool inputDown;
    private bool playDoorSound = false;
    public int TranslateSpeed = 4;
    public GameObject leftDoor;
    public GameObject rightDoor;
    public AudioSource Opening;
    public AudioSource Closing;
    private InteractibleRaycast playerCast;
    private string doorPrompt = "E to open/close door";
    private TMP_Text InteractPrompt;
    // Start is called before the first frame update
    void Start()
    {
        //playerCast = GameObject.FindObjectOfType<InteractibleRaycast>();
        InteractPrompt = GameObject.Find("UI_Prompts").GetComponent<TMP_Text>();
        InteractPrompt.text = doorPrompt;
        InteractPrompt.enabled = false;
        PosLDoor = leftDoor.transform.localPosition;
        PosRDoor = rightDoor.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Interact"))
        {
            inputDown = true;
        }
        
        //buttonDoorPrompt();
        openCloseSequence();
        doorSounds();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") == true && isAutoDoor == true)
        {
            openDoor = true;
            playDoorSound = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        InteractPrompt.enabled = false;
        if (other.gameObject.CompareTag("Player") == true && isAutoDoor == true)
        {
            openDoor = false;
            playDoorSound = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && isAutoDoor == false)
        {
            InteractPrompt.enabled = true;
            if (inputDown == true)
            {
                openDoor = !openDoor;
                playDoorSound = !playDoorSound;
                inputDown = false;
            }
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
            else
            {
                playDoorSound = false;
            }
       

            if (leftDoor.transform.localPosition.x < PosLDoor.x)
            {
                leftDoor.transform.Translate(TranslateDirection * Time.deltaTime * TranslateSpeed);
            }
            else
            {
                playDoorSound = false;
            }
            
        }
        else
        {
            if (rightDoor.transform.localPosition.x < PosRDoor.x + 1)
            {
                rightDoor.transform.Translate(TranslateDirection * Time.deltaTime * TranslateSpeed);
            }
            else
            {
                playDoorSound = false;
            }

            if (leftDoor.transform.localPosition.x > PosLDoor.x - 1)
            {
                leftDoor.transform.Translate(-TranslateDirection * Time.deltaTime * TranslateSpeed);
            }
            else
            {
                playDoorSound = false;
            }
        }
    }

    private void doorSounds()
    {
        if (playDoorSound == false) { return; }

        if (openDoor == false && Closing.isPlaying == false)
        {
            Closing.Play();
            Opening.Stop();
        }
        else if (openDoor == true && Opening.isPlaying == false)
        {
            Opening.Play();
            Closing.Stop();
        }
    }
}
