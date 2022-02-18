using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterBrain : MonoBehaviour
{
    public Transform otherPortal;
    public AudioSource portalAmbient;
    public AudioSource warpingSound;
    private Vector3 warpPos;
    // Start is called before the first frame update
    void Start()
    {
        portalAmbient.loop = true;
        portalAmbient.playOnAwake = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.position.z > this.transform.position.z)
        {
            warpPos = new Vector3(otherPortal.position.x, otherPortal.position.y, otherPortal.position.z - 1);
        }
        else
        {
            warpPos = new Vector3(otherPortal.position.x, otherPortal.position.y, otherPortal.position.z + 1);
        }
        
        if (other.gameObject.CompareTag("Player"))
        {
            warpingSound.Play();
            other.gameObject.GetComponent<CharacterController>().enabled = false;
            other.gameObject.transform.SetPositionAndRotation(warpPos, other.gameObject.transform.rotation);
            other.gameObject.GetComponent<CharacterController>().enabled = true;
        }
    }
}
