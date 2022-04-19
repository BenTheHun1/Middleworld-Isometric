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
    Transform standpoint;
    string queueblock;

    public Texture2D curNorm;
    public Texture2D curSel;

    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.gameObject.GetComponent<NavMeshAgent>();
        animator = gameObject.GetComponent<Animator>();
        pointer.position = away.position;
        Cursor.SetCursor(curNorm, new Vector2(7, 7), CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, ground))
        {
            targetPosition = hit.point;
            if (hit.collider.gameObject.CompareTag("Object"))
            {
                Cursor.SetCursor(curSel, new Vector2(7, 7), CursorMode.Auto);
            }
            else
            {
                Cursor.SetCursor(curNorm, new Vector2(7, 7), CursorMode.Auto);
            }
        }

        if (Input.GetKey(KeyCode.Mouse0)) //GetKey to Drag, GetKeyDown to Move to point. Have to weight pros and cons
        {
            if (hit.collider.gameObject.CompareTag("Object"))
            {
                
                standpoint = hit.collider.gameObject.transform.Find("StandPoint");
                if (standpoint != null && !flow.HasExecutingBlocks())
                {
                    queueblock = hit.collider.gameObject.name;
                    agent.destination = standpoint.transform.position;
                    pointer.position = standpoint.transform.position;
                }
            }
            else if (!flow.HasExecutingBlocks())
            {
                queueblock = null;
                agent.destination = targetPosition;
                pointer.position = targetPosition;
                
            }
        }
        animator.SetFloat("velocity", agent.velocity.magnitude);
        //Debug.Log(agent.remainingDistance);
        if (ReachedDestinationOrGaveUp())
        {
            pointer.position = away.position;
            if (queueblock != null)
            {
                flow.ExecuteBlock(queueblock);
                queueblock = null;
                StartCoroutine("Rotate");
            }
        }
        else
        {
            //pointer.position = agent.destination;
        }
    }

    IEnumerator Rotate()
    {
        for (float i = 0;  gameObject.transform.rotation != standpoint.rotation; i += Time.deltaTime)
        {
            gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, Mathf.Lerp(gameObject.transform.eulerAngles.y, standpoint.eulerAngles.y, i), gameObject.transform.eulerAngles.z);

            yield return null;
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
