using UnityEngine;
using UnityEngine.AI;

public class NPCPatrol : MonoBehaviour
{
    public NPCWaypoint[] waypoints;

    private NavMeshAgent agent;
    private Animator animator;

    private NPCWaypoint currentWaypoint;

    private float waitTimer;
    private bool waiting;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        GoToNextWaypoint();
    }

    void Update()
    {
        if (waiting)
        {
            waitTimer -= Time.deltaTime;

            if (waitTimer <= 0)
            {
                waiting = false;
                GoToNextWaypoint();
            }

            return;
        }

        // Added !waiting check here to prevent the arrival logic 
        // from re-triggering and looping animator.Play() every frame
        if (!waiting && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            waiting = true;
            waitTimer = currentWaypoint.waitTime;

            agent.ResetPath();

            animator.Play(currentWaypoint.animationName);
        }
    }

    void GoToNextWaypoint()
    {
        if (waypoints.Length == 0)
            return;

        NPCWaypoint next;

        do
        {
            next = waypoints[Random.Range(0, waypoints.Length)];
        }
        while (next == currentWaypoint && waypoints.Length > 1);

        currentWaypoint = next;
        agent.SetDestination(currentWaypoint.transform.position);

        // Ensures the walk animation triggers seamlessly whenever moving to a new spot
        animator.Play("Walk");
    }
}