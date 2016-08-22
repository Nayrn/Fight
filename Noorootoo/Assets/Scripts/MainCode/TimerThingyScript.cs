using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimerThingyScript : MonoBehaviour
{

    public float TimerThing;
    public Text TimeText;

    // Use this for initialization
    void Start()
    {
        TimerThing = 30.0f;

    }

    // Update is called once per frame
    void Update()
    {

        int timer = (int)TimerThing;
        TimeText.text = timer.ToString();
        TimerThing -= Time.deltaTime;
        if(TimerThing < 0)
        {
            TimerThing = 0;
        }
    }
}
