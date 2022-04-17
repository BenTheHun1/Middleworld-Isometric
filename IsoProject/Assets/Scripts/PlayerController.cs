using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    Vector3 targetPosition;
    NavMeshAgent agent;
    public LayerMask ground;
    RaycastHit hit;
    public Transform pointer;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.gameObject.GetComponent<NavMeshAgent>();
        animator = gameObject.GetComponent<Animator>();
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

            agent.destination = targetPosition;
            pointer.position = targetPosition;
        }
        animator.SetFloat("velocity", agent.velocity.magnitude);
    }
}
