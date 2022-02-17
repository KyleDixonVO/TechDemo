using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour

{
    private Vector3 PosRDoor;
    private Vector3 PosLDoor;
    private Vector3 MaxDoorTranslate = new Vector3(0.5f, 0, 0);
    public bool openDoor = false;
    public int TranslateSpeed = 8;
    public GameObject leftDoor;
    public GameObject rightDoor;
    // Start is called before the first frame update
    void Start()
    {
        PosLDoor = leftDoor.transform.localPosition;
        PosRDoor = rightDoor.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (openDoor == false)
        {
            if (rightDoor.transform.localPosition.x > PosRDoor.x)
            {
                rightDoor.transform.Translate(-MaxDoorTranslate * Time.deltaTime * TranslateSpeed);
            }

            if (leftDoor.transform.localPosition.x < PosLDoor.x)
            {
                leftDoor.transform.Translate(MaxDoorTranslate * Time.deltaTime * TranslateSpeed);
            }
        }
        else
        {
            if (rightDoor.transform.localPosition.x < PosRDoor.x + 1)
            {
                rightDoor.transform.Translate(MaxDoorTranslate * Time.deltaTime * TranslateSpeed);
            }

            if (leftDoor.transform.localPosition.x > PosLDoor.x - 1)
            {
                leftDoor.transform.Translate(-MaxDoorTranslate * Time.deltaTime * TranslateSpeed);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            openDoor = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            openDoor = false;
        }
    }
}
