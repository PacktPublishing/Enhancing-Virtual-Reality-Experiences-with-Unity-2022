using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(NavMeshAgent))]
public class TrafficAI : MonoBehaviour
{
    public float speed = 10f; // speed of the car
    public float turnSpeed = 2f; // speed at which the car turns
    public float turnRadius = 5f; // radius of the car's turning circle
    public AudioClip engineSound; // engine sound clip
    public AudioClip honkSound; // honking sound clip
    public float honkChance = 0.1f; // chance of honking at each waypoint

    private List<Vector3> waypoints; // list of waypoints for the car to follow
    private bool turning; // whether the car is currently turning or not
    private AudioSource audioSource; // audio source component
    private NavMeshAgent navMeshAgent; // NavMeshAgent component
    private int currentWaypointIndex = -1; // index of the current waypoint the car is heading towards

    // Start is called before the first frame update
    void Start()
    {
        // get the AudioSource component and start playing the engine sound
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = engineSound;
        audioSource.loop = true;
        audioSource.Play();

        // get the NavMeshAgent component
        navMeshAgent = GetComponent<NavMeshAgent>();

        // set the NavMeshAgent properties
        navMeshAgent.speed = speed;
        navMeshAgent.angularSpeed = turnSpeed * Mathf.Rad2Deg;
        navMeshAgent.radius = turnRadius;
        navMeshAgent.autoTraverseOffMeshLink = false;

        // find a random point on the NavMesh and set it as the destination
        FindRandomNavMeshPoint();
    }

    // Update is called once per frame
    void Update()
    {
        // check if the car has reached the current waypoint
        if (navMeshAgent.pathPending || navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
        {
            return;
        }

        // start turning towards the next waypoint
        StartCoroutine(TurnTowardsWaypoint());

        // play a honking sound with a random chance
        if (Random.value < honkChance)
        {
            audioSource.PlayOneShot(honkSound);
        }
    }

    IEnumerator TurnTowardsWaypoint()
    {
        // set the turning flag to true
        turning = true;

        // get the current waypoint position
        Vector3 currentWaypoint = waypoints[currentWaypointIndex];

        // calculate the direction from the car to the current waypoint
        Vector3 targetDirection = currentWaypoint - transform.position;
        targetDirection.y = 0f;

        // calculate the angle between the car's forward vector and the direction to the current waypoint
        float angle = Vector3.SignedAngle(transform.forward, targetDirection, Vector3.up);

        // calculate the turn center point
        Vector3 turnCenter = transform.position + Quaternion.Euler(0f, angle, 0f) * transform.forward * navMeshAgent.radius;

        // calculate the target angle for the car to face towards the turn center point
        float targetAngle = Mathf.Atan2(turnCenter.x - transform.position.x, turnCenter.z - transform.position.z) * Mathf.Rad2Deg;

        while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, targetAngle)) > 0.05f)
        {
            // calculate the angle to turn towards the turn center point
            float angleToTurn = turnSpeed * Time.deltaTime * Mathf.Sign(Mathf.DeltaAngle(transform.eulerAngles.y, targetAngle));

            // calculate the new turn center point based on the updated angle to turn
            turnCenter = transform.position + Quaternion.Euler(0f, angleToTurn, 0f) * (turnCenter - transform.position);

            // move the car along the turning circle
            transform.position = turnCenter + Quaternion.Euler(0f, angleToTurn - targetAngle, 0f) * (transform.position - turnCenter);

            yield return null;
        }

        // set the NavMeshAgent destination to a new random point on the NavMesh
        FindRandomNavMeshPoint();

        // set the turning flag to false
        turning = false;
    }

    private void FindRandomNavMeshPoint()
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 10f, NavMesh.AllAreas))
        {
            NavMeshPath path = new NavMeshPath();
            NavMesh.CalculatePath(hit.position, RandomNavMeshPoint(hit.position, 10f), NavMesh.AllAreas, path);

            if (path.corners.Length > 0)
            {
                currentWaypointIndex = 0;
                waypoints = new List<Vector3>(path.corners);
                navMeshAgent.SetDestination(waypoints[currentWaypointIndex]);
            }
        }
    }

    private Vector3 RandomNavMeshPoint(Vector3 center, float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += center;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, radius, NavMesh.AllAreas);
        return hit.position;
    }
}
/*
 * 
 * 
This version of the script replaces the list of waypoints with a NavMeshAgent component, which allows the car to follow a path on the NavMesh instead of a predefined set of waypoints. When the script starts, it finds a random point on the NavMesh and sets it as the NavMeshAgent's destination. Once the car reaches the destination, it finds another random point on the NavMesh and sets it as the new destination, and so on.

The script also adds a new function, RandomNavMeshPoint, which takes a center point and a radius and returns a random point on the NavMesh within the given radius of the center point. This function is used to find the initial random point and to find new random points after the car reaches its current destination.

Finally, the script updates the TurnTowardsWaypoint coroutine to use the NavMeshAgent component to move the car instead of directly manipulating its position and rotation. The coroutine now calculates the turn center point based on the NavMeshAgent's radius property and uses the NavMeshAgent's angularSpeed property to control the car's turning speed.
*/