using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravWell : MonoBehaviour
{
    private GameObject player;
    private bool MoveCloser;
    private bool MoveFurther;
    private CastGravWell caster;
    public AudioSource disturbance;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("FPSController");
        caster = GameObject.Find("FPSController").GetComponent<CastGravWell>();
        disturbance.Play();
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("FPSController");
        caster = GameObject.Find("FPSController").GetComponent<CastGravWell>();
        if (caster.summoned == false)
        {
            Physics.autoSyncTransforms = false;
            int children = this.transform.childCount;
            for (int i = 0; i < children; i++)
            {
                Rigidbody rb; 
                if (this.transform.GetChild(i).TryGetComponent<Rigidbody>(out rb) == true)
                {
                    rb.isKinematic = false;
                    rb.useGravity = true;
                }
                this.transform.GetChild(i).transform.parent = null;
            }
            Destroy(this.gameObject);
        }

        //if (Input.GetKey(KeyCode.Mouse0))
        //{
        //    MoveCloser = true;

        //}
        //else
        //{
        //    MoveCloser = false;
        //}

        if (Input.GetKey(KeyCode.Mouse1))
        {
            int children = this.transform.childCount;
            for (int i = 0; i < children; i++)
            {
                Rigidbody rb;
                this.transform.GetChild(i).TryGetComponent<Rigidbody>(out rb);
                rb.isKinematic = false;
                rb.useGravity = true;
                rb.AddForce(caster.playerCamTransform.forward * 100, ForceMode.Impulse);
                this.transform.GetChild(i).transform.parent = null;

            }
        }
    }

    private void FixedUpdate()
    {
        //MoveWellCloser();

    }

    private void OnTriggerEnter(Collider other)
    {
       if (other.gameObject.layer == 6)
       {
            Physics.autoSyncTransforms = true;
            other.transform.parent = this.gameObject.transform;
            Rigidbody rb;
            if (other.gameObject.TryGetComponent<Rigidbody>(out rb) == true)
            {
                rb.useGravity = false;
                rb.isKinematic = true;
            }
       }
    }

    public void MoveWellCloser()
    {
        if (MoveCloser == true)
        {
            this.transform.Translate(-caster.playerCamTransform.forward);
        }
    }
}
