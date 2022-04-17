using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 defaultPosition;

    // Start is called before the first frame update
    void Start()
    {
        defaultPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = player.transform.position + defaultPosition;
        //Debug.Log(player.transform.position + " " + defaultPosition);
    }
}
