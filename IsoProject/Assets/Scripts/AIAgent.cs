using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIAgent : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    public GameObject positionsParent;

    private int destPoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = gameObject.GetComponent<Animator>();
        for (int i = 0; i < positionsParent.transform.childCount; i++)
        {
            positionsParent.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
        NextPoint();
    }

    void NextPoint()
    {
        //Debug.Log(destPoint);
        // Returns if no points have been set up
        if (positionsParent.transform.childCount == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = positionsParent.transform.GetChild(destPoint).position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.


    }
    private void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            destPoint = (destPoint + 1) % positionsParent.transform.childCount;
            NextPoint();
        }
        else
        {
            Debug.DrawLine(transform.transform.position, positionsParent.transform.GetChild(destPoint).position);
        }
        animator.SetFloat("velocity", agent.velocity.magnitude);
    }
}
