using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerAway : MonoBehaviour
{

    public Transform away;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = away.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            gameObject.transform.position = away.position;
        }
    }
}
