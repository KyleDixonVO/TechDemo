using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spotting : MonoBehaviour
{
    public bool inSightRadius = false;
    private BotPathfinding pathfinding;
    // Start is called before the first frame update
    void Start()
    {
        pathfinding = GameObject.Find("Bot").GetComponent<BotPathfinding>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (pathfinding.spottedPlayer == false)
        {
            inSightRadius = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inSightRadius = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inSightRadius = false;
        }
    }
}
