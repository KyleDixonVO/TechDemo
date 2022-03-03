using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float deltaX = 1;
    public float deltaY = 0;
    public float deltaZ = 0;

    //Movement clamps, should always be positive when using local position.
    public float clampX = 10;
    public float clampY = 10;
    public float clampZ = 10;
    public GameObject targetObject = null;
    private Vector3 endPosition;
    private float cooldownTimer;
    private float resetDelay = 0.2f;
    public Vector3 platformMovement;

    private bool finishedX;
    private bool finishedY;
    private bool finishedZ;
    public bool invertMovement = false;
    public bool invertOnCollisions = true;
    public bool useLocalPosition = true;
    public bool oneWay;
    // Start is called before the first frame update
    void Start()
    {
        if (targetObject != null)
        {
            targetObject.transform.position = endPosition;
        }
        platformMovement.x = deltaX;
        platformMovement.y = deltaY;
        platformMovement.z = deltaZ;
    }

    // Update is called once per frame
    void Update()
    {
        if (invertOnCollisions == false)
        {
            translateToClamps();
        }

        if (invertMovement == true)
        {
            this.gameObject.transform.Translate(-platformMovement * Time.deltaTime);
        }
        else
        {
            this.gameObject.transform.Translate(platformMovement * Time.deltaTime);
        }  
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) { return; }

        if (invertOnCollisions == true)
        {
            invertMovement = !invertMovement;
        }
        
        if(oneWay == true)
        {
            platformMovement = Vector3.zero;
        }
    }

    private void translateToClamps()
    {
        if (Time.time <= cooldownTimer + resetDelay) { return; }
        if (useLocalPosition == true)
        {
            if (Mathf.Abs(this.gameObject.transform.localPosition.x) >= clampX || deltaX == 0)
            {
                platformMovement.x = 0;
                finishedX = true;
            }

            if(Mathf.Abs(this.gameObject.transform.localPosition.y) >= clampY || deltaY == 0)
            {
                platformMovement.y = 0;
                finishedY = true;
            }

            if (Mathf.Abs(this.gameObject.transform.localPosition.z) >= clampZ || deltaZ == 0)
            {
                platformMovement.z = 0;
                finishedZ = true;
            }
        }
        else
        {
            if (Mathf.Abs(this.gameObject.transform.position.x) == Mathf.Abs(endPosition.x) || deltaX == 0)
            {
                platformMovement.x = 0;
                finishedX = true;
            }

            if (Mathf.Abs(this.gameObject.transform.position.y) == Mathf.Abs(endPosition.y) || deltaY == 0)
            {
                platformMovement.y = 0;
                finishedY = true;
            }

            if (Mathf.Abs(this.gameObject.transform.position.z) == Mathf.Abs(endPosition.z) || deltaZ == 0)
            {
                platformMovement.z = 0;
                finishedZ = true;
            }
        }

        if (finishedX == true && finishedY == true && finishedZ == true && oneWay == false)
        {
            cooldownTimer = Time.time;
            finishedX = false;
            finishedY = false;
            finishedZ = false;
            platformMovement.x = deltaX;
            platformMovement.y = deltaY;
            platformMovement.z = deltaZ;
            invertMovement = !invertMovement;
        }
    }
}
