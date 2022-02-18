using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killbox : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 defaultSpawn = new Vector3(0,0,0);
    private Vector3 newSpawn;
    private Quaternion startRot = Quaternion.Euler(Vector3.zero);
    private GameObject player;
    private CheckpointManager manager;
    public AudioSource deathSound;


    // enabling-disabling Character Controller is not a hack, Character Controllers *ARE* colliders, and are unaffected by forces.
    // the Character Controller cannot be teleported (not the same as instantanious movement) due to it tracking it's own movement internally.
    // to achieve "teleportation", or instantaneous movement through objects you cannot collide with them, thus the colliders are disabled for a frame.
    // the Unity Docs recommend using a rigidbody system if programmers wish to maintain movement input all the time
    // https://docs.unity3d.com/Manual/class-CharacterController.html


    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("CManager").GetComponent<CheckpointManager>();
        newSpawn = defaultSpawn;
    }

    // Update is called once per frame
    void Update()
    {
       if (manager.lastCheckpoint != null)
       {
            newSpawn = manager.lastCheckpoint;
       }
       else
       {
            newSpawn = defaultSpawn;
       }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Triggered Killbox");
            player = other.gameObject;
            player.GetComponent<CharacterController>().enabled = false;
            player.transform.SetPositionAndRotation(newSpawn, startRot);
            player.GetComponent<CharacterController>().enabled = true;
            deathSound.Play();
        }
    }
}
