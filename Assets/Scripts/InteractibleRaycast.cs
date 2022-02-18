using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleRaycast : MonoBehaviour
{
    public bool withinRange;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
       withinRange = Physics.Raycast(this.transform.position, this.transform.TransformDirection(Vector3.forward), 5.0f, 3, QueryTriggerInteraction.Ignore);
    }
}
