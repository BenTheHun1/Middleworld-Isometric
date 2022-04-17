using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Fungus;

public class PlayerController : MonoBehaviour
{
    public Flowchart flow;
    Vector3 targetPosition;
    NavMeshAgent agent;
    public LayerMask ground;
    RaycastHit hit;
    public Transform pointer;
    Animator animator;
    public Transform away;

    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.gameObject.GetComponent<NavMeshAgent>();
        animator = gameObject.GetComponent<Animator>();
        pointer.position = away.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, ground))
            {
                targetPosition = hit.point;
            }
            //gameObject.transform.position = targetPosition;

            if (hit.collider.gameObject.CompareTag("Object"))
            {
                flow.ExecuteBlock(hit.collider.gameObject.name);
                Transform tmp = hit.collider.gameObject.transform.Find("StandPoint");
                if (tmp != null)
                {
                    agent.destination = tmp.transform.position;
                    pointer.position = tmp.transform.position;
                }
            }
            else if (!flow.HasExecutingBlocks())
            {
                agent.destination = targetPosition;
                pointer.position = targetPosition;
                
            }
        }
        animator.SetFloat("velocity", agent.velocity.magnitude);
        Debug.Log(agent.remainingDistance);
        if (ReachedDestinationOrGaveUp())
        {
            pointer.position = away.position;
        }
        else
        {
            //pointer.position = agent.destination;
        }
    }

    public bool ReachedDestinationOrGaveUp()
    {

        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
