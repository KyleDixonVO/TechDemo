using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private CheckpointManager manager;
    public Material activated;
    public Material inactive;
    public AudioSource checkpointSound;
    private bool isActive;

    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("CManager").GetComponent<CheckpointManager>();
    }

    private void Update()
    {
        if (manager.lastCheckpoint != this.transform.position)
        {
            this.GetComponent<Renderer>().material = inactive;
            isActive = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            manager.lastCheckpoint = this.transform.position;
            this.GetComponent<Renderer>().material = activated;
            if (isActive == false)
            {
                checkpointSound.Play();
            }
            isActive = true;
        }
    }
}
