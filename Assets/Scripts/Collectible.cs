using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private float countdownTimer;
    public float respawnDelay = 2;
    public float elapsedTime = 0;
    public float rotationRateX = 1;
    public float rotationRateY = 0;
    public float rotationRateZ = 1;
    Quaternion collectibleRotation;
    private bool active = true;
    public bool respawnable = true;
    public AudioSource collectibleSound;
    private ScoreHandler handler;
    // Start is called before the first frame update
    void Start()
    {
        handler = GameObject.FindObjectOfType<ScoreHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime = Time.time;
        collectibleRotation = Quaternion.Euler(rotationRateX * elapsedTime, rotationRateY * elapsedTime, rotationRateZ * elapsedTime);
        if (countdownTimer + respawnDelay <= elapsedTime && respawnable == true)
        {
            this.gameObject.GetComponent<Renderer>().enabled = true;
            active = true;
        }

        if (active == true)
        {
            this.gameObject.transform.rotation = collectibleRotation;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (active == true)
            {
                handler.score++;
                this.gameObject.GetComponent<Renderer>().enabled = false;
                collectibleSound.Play();
                active = false;
                if (respawnable == true)
                {
                    countdownTimer = Time.time;
                }
            }
        }
    }
}
