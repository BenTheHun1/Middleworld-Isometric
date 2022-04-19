using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeSys : MonoBehaviour
{
    public int day;
    public int hr;
    public int min;
    public int sec;

    TextMeshProUGUI txt;
    float progmin;
    float delay = 3;
    // Start is called before the first frame update
    void Start()
    {
        txt = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        txt.text = hr.ToString() + ":" + min.ToString("00");
        progmin += Time.deltaTime / delay; // delay = number of seconds to mins
        if (progmin > 1)
        {
            min++;
            progmin--;
            if (min >= 60)
            {
                hr++;
                min -= 60;
                if (hr >= 13)
                {
                    day++;
                    hr -= 13;
                }
            }
        }
    }
}
