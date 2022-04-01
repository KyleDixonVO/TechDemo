using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotPathfinding : MonoBehaviour
{
    public enum botStates
    {
        patrolling = 0,
        searching = 1,
        chasing = 2,
        attacking = 3,
        retreating = 4
    }

    public LayerMask botMask;
    private GameObject patrolParent;
    private Transform[] patrolPoints;
    private GameObject player;
    private NavMeshAgent agent;
    private int nextPoint = 0;
    public botStates currentState;
    public Vector3 lastSeen;
    public Vector3 playerPos;
    private detection detectionRadius;
    private spotting spottingRadius;
    private Transform closestTarget;
    //private int lastPoint;

    //Spotting floats
    public float unspotDelay = 5.0f;
    public float timeToUnspot;
    public float time;

    //Bools
    public bool spottedPlayer = false;
    public bool timerFinished;
    private bool reachedNearestPatrolPoint;

    // Start is called before the first frame update
    void Start()
    {
        detectionRadius = GameObject.Find("DetectionRadius").GetComponent<detection>();
        spottingRadius = GameObject.Find("SpottingRadius").GetComponent<spotting>();
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        patrolParent = GameObject.Find("PatrolPoints");
        patrolPoints = new Transform[patrolParent.transform.childCount];
        currentState = botStates.patrolling;

        for (int i = 0; i < patrolParent.transform.childCount; i++)
        {
            patrolPoints[i] = patrolParent.transform.GetChild(i).transform;
        }
        player = GameObject.Find("FPSController");
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;
        time = Time.time;
        EvaluateState();
        SpotTimer();
        switch (currentState)
        {
            case botStates.patrolling:
                this.GetComponent<Renderer>().material.color = Color.blue;
                Patrolling();
                break;

            case botStates.searching:
                this.GetComponent<Renderer>().material.color = Color.green;
                Searching();
                break;

            case botStates.chasing:
                this.GetComponent<Renderer>().material.color = Color.yellow;
                Chasing();
                break;

            case botStates.attacking:
                this.GetComponent<Renderer>().material.color = Color.red;
                Attacking();
                break;

            case botStates.retreating:
                this.GetComponent<Renderer>().material.color = Color.white;
                Retreating();
                break;


        }
    }

    public void Patrolling()
    {
        agent.SetDestination(patrolPoints[nextPoint].transform.position);
        if (agent.transform.position.x == patrolPoints[nextPoint].transform.position.x && agent.transform.position.z == patrolPoints[nextPoint].transform.position.z)
        {
            if (nextPoint < patrolPoints.Length - 1) nextPoint++;
            else nextPoint = 0;
        }
    }

    public void Searching()
    {
        agent.SetDestination(lastSeen);
    }

    public void Chasing()
    {
        agent.SetDestination(player.transform.position);
    }

    public void Attacking()
    {
        agent.SetDestination(agent.transform.position);
    }

    public void Retreating()
    {
        ClosestPatrolPoint();
        agent.SetDestination(closestTarget.position);
        if (agent.transform.position.x == closestTarget.position.x && agent.transform.position.z == closestTarget.position.z)
        {
            reachedNearestPatrolPoint = true;
        }
        else
        {
            reachedNearestPatrolPoint = false;
        }
    }

    public void SpotTimer()
    {
        if (currentState != botStates.searching)
        {
            timeToUnspot = Time.time + unspotDelay;
            timerFinished = false;
        }
        else
        {
            if (Time.time > timeToUnspot)
            {
                timerFinished = true;
            }
        }
    }

    public void ClosestPatrolPoint()
    {
        closestTarget = null;
        float closestDistSqr = Mathf.Infinity;

        for (int i = 0; i < patrolPoints.Length; i++)
        {
            Vector3 directionToPoint = patrolPoints[i].transform.position - agent.transform.position;
            float distSqrToPoint = directionToPoint.sqrMagnitude;

            if (distSqrToPoint < closestDistSqr)
            {
                closestDistSqr = distSqrToPoint;
                closestTarget = patrolPoints[i].transform;
                nextPoint = i;
            }
        }
    }

    public void EvaluateState()
    {
        if (Physics.Linecast(agent.transform.position, player.transform.position, botMask) == true)
        {
            spottedPlayer = false;
        }
        else
        {
            spottedPlayer = true;
        }

        if (spottedPlayer == true && spottingRadius.inSightRadius == true)
        {
            lastSeen = player.transform.position;
        }

        //patrolling to chasing
        if (currentState == botStates.patrolling && detectionRadius.inDetectionRadius == true && spottedPlayer == true)
        {
            currentState = botStates.chasing;
        }

        //chasing to searching
        if (currentState == botStates.chasing && (spottingRadius.inSightRadius == false || spottedPlayer == false))
        {
            currentState = botStates.searching;
        }

        //chasing to attacking
        if (currentState == botStates.chasing && spottedPlayer == true && detectionRadius.inDetectionRadius == true)
        {
            currentState = botStates.attacking;
        }

        //searching to retreating
        if (currentState == botStates.searching && timerFinished == true)
        {
            currentState = botStates.retreating;
        }

        //searching to chasing
        if (currentState == botStates.searching && spottedPlayer == true && spottingRadius.inSightRadius == true)
        {
            currentState = botStates.chasing;
        }

        //retreating to patrolling
        if (currentState == botStates.retreating && reachedNearestPatrolPoint == true)
        {
            currentState = botStates.patrolling;
        }

        //retreating to chasing
        if (currentState == botStates.retreating && spottedPlayer == true && spottingRadius.inSightRadius == true)
        {
            currentState = botStates.chasing;
        }

        //attacking to chasing
        if (currentState == botStates.attacking && spottedPlayer == true && spottingRadius.inSightRadius == true && detectionRadius.inDetectionRadius == false)
        {
            currentState = botStates.chasing;
        }

        //attacking to searching
        if (currentState == botStates.attacking && spottedPlayer == false)
        {
            currentState = botStates.searching;
        }
    }
}
