using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterCam : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localRotation = GameObject.FindGameObjectWithTag("Player").transform.rotation;
    }
}
