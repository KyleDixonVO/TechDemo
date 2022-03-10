using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastGravWell : MonoBehaviour
{
    // Start is called before the first frame update
    public RaycastHit gravHit;
    public Ray gravCast;
    public Vector3 gravCastPos;
    public float castDistance = 30;
    private Vector3 castOrigin;
    private Transform playerCamTransform;
    public GameObject gravWell;
    public GameObject gravInstance;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        playerCamTransform = GameObject.Find("FirstPersonCharacter").GetComponent<Camera>().transform;
        castOrigin = new Vector3(playerCamTransform.position.x, playerCamTransform.position.y, playerCamTransform.position.z);
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (CastWell() == true)
            {
                Vector3 gravOrigin = new Vector3(gravHit.transform.position.x, gravHit.transform.position.y, gravHit.transform.position.z);
                gravInstance = Instantiate(gravWell, gravOrigin, Quaternion.identity, this.gameObject.transform);
            }
            else
            {
                gravCast = new Ray(new Vector3(playerCamTransform.transform.position.x, playerCamTransform.transform.position.y, playerCamTransform.transform.position.z), playerCamTransform.forward);
                gravCastPos = gravCast.GetPoint(30);
                gravInstance = Instantiate(gravWell, new Vector3( gravCastPos.x, gravCastPos.y, gravCastPos.z), Quaternion.identity, this.gameObject.transform);
                Debug.Log("cast in open space");
            }
        }

        if (gravInstance != null)
        {
           
        }
        
        if (Input.GetKeyUp(KeyCode.G))
        {
            Destroy(gravInstance);
        }
    }

    public bool CastWell()
    {
       
       if (Physics.Raycast(castOrigin, playerCamTransform.TransformDirection(Vector3.forward), out gravHit, castDistance, ~2))
       {

            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * gravHit.distance, Color.green);
            Debug.Log("raycast hit");
            return true;  
       }
       else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * castDistance, Color.red);
            Debug.Log("no hit");
            return false;
        }
        
    }
}
