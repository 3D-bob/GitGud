using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

//Made by Tuomas Ahonen

public class Timer : MonoBehaviour {

    public Text timer;
    float time;
    int timeInt;

    void Start()
    {
        time = 0;
        UpdateTimer();     
    }

    void Update()
    {
        time += Time.deltaTime;
        UpdateTimer();
        timeInt = Mathf.RoundToInt(time);
    }

    void UpdateTimer()
    {
        timer.text = "Time: " + timeInt.ToString();
    }
}
