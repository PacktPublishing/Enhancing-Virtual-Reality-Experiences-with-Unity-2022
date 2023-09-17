using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speed = 10f; // speed of the car
    public float turnSpeed = 2f; // speed at which the car turns
    public float turnRadius = 5f; // radius of the car's turning circle
    public List<Transform> waypoints; // list of waypoints for the car to follow
    public AudioClip honkSound; // honking audio clip
    public float honkChance = 0.1f; // chance of honking at each waypoint

    private int currentWaypointIndex = 0; // index of the current waypoint the car is heading towards
    private bool turning; // whether the car is currently turning or not

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        // set the target position to the position of the first waypoint
        transform.position = waypoints[0].position;

        // get the AudioSource component
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // if the car is not turning, move it towards the target position
        if (!turning)
        {
            transform.position += transform.forward * speed * Time.deltaTime;

            // check if the car has reached the current waypoint
            if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.5f)
            {
                // move to the next waypoint
                currentWaypointIndex++;

                // if the car has reached the last waypoint, reset to the first waypoint
                if (currentWaypointIndex >= waypoints.Count)
                {
                    currentWaypointIndex = 0;
                }

                // start turning towards the next waypoint
                StartCoroutine(TurnTowardsWaypoint());

                // play a honking sound with a random chance
                if (Random.value < honkChance)
                {
                    audioSource.PlayOneShot(honkSound);
                }
            }
        }
    }

    IEnumerator TurnTowardsWaypoint()
    {
        // set the turning flag to true
        turning = true;

        // get the position of the next waypoint
        Vector3 targetPosition = waypoints[currentWaypointIndex].position;

        // calculate the direction from the car to the next waypoint
        Vector3 targetDirection = targetPosition - transform.position;
        targetDirection.y = 0f;

        // calculate the angle between the car's forward vector and the direction to the next waypoint
        float angle = Vector3.SignedAngle(transform.forward, targetDirection, Vector3.up);

        // calculate the turn radius based on the car's speed
        float turnRadius = Mathf.Abs(Mathf.Pow(speed, 2f) / (2f * angle * Mathf.Deg2Rad));

        // calculate the turn center point
        Vector3 turnCenter = transform.position + Quaternion.Euler(0f, angle, 0f) * transform.forward * turnRadius;

        // calculate the target angle for the car to face towards the turn center point
        float targetAngle = Mathf.Atan2(turnCenter.x - transform.position.x, turnCenter.z - transform.position.z) * Mathf.Rad2Deg;

        // rotate the car towards the turn center point
        while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, targetAngle)) > 1f)
        {
            float angleToTurn = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, turnSpeed * Time.deltaTime);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, angleToTurn, transform.eulerAngles.z);
            // move the car along the turning circle
            transform.position = turnCenter + Quaternion.Euler(0f, angleToTurn - targetAngle, 0f) * (transform.position - turnCenter);

            yield return null;
        }

        // set the new target position to the position of the next waypoint
        transform.LookAt(targetPosition);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

        // set the turning flag to false
        turning = false;
    }
}

