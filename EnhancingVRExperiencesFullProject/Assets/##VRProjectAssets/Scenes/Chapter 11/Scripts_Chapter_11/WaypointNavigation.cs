using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointNavigation : MonoBehaviour
{
    public List<Transform> waypoints;  // List of waypoints to navigate to
    public NavMeshAgent agent;        // Reference to the NavMeshAgent component
    public int currentWaypointIndex;  // Index of the current waypoint in the list

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentWaypointIndex = 0;

        // Check if the NavMeshAgent is on a valid NavMesh
        if (!agent.isOnNavMesh)
        {
            Debug.LogError("NavMeshAgent not on a valid NavMesh!");
            return;
        }

        NavigateToNextWaypoint();
    }

    //void NavigateToNextWaypoint()
    //{
    //    if (waypoints.Count == 0)
    //    {
    //        Debug.LogError("No waypoints set!");
    //        return;
    //    }

    //    // Set the destination of the NavMeshAgent to the current waypoint
    //    agent.SetDestination(waypoints[currentWaypointIndex].position);

    //    // Increment the index of the current waypoint, or reset to zero if we've reached the end of the list
    //    currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
    //}

    void NavigateToNextWaypoint()
    {
        if (waypoints.Count == 0)
        {
            Debug.LogError("No waypoints set!");
            return;
        }

        

        // If we've reached the end of the list, choose a random waypoint to navigate to
        if (currentWaypointIndex == waypoints.Count - 1)
        {
            currentWaypointIndex = Random.Range(0, waypoints.Count);
        }
        // Otherwise, increment the index of the current waypoint
        else
        {
            currentWaypointIndex++;
        }
    }


    void Update()
    {
        // If the NavMeshAgent has reached its destination, navigate to the next waypoint
        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            NavigateToNextWaypoint();
            agent.SetDestination(waypoints[currentWaypointIndex].position);
        }
    }


}
