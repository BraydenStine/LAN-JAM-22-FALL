using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SpeedRunTimer : MonoBehaviour
{
    private float speedRunTime = 0f;
    private TMP_Text speedRunText;

    private void Start()
    {
        speedRunText = GetComponent<TMP_Text>();
        speedRunTime = 0f;
    }

    private void Update()
    {
        TickSpeedRun();
    }

    void TickSpeedRun()
    {
        speedRunTime += Time.deltaTime;
        speedRunText.text =(int)(speedRunTime / 60) + ":";
        if ((int)((speedRunTime % 60) - 1) < 10)
            speedRunText.text += "0";
        speedRunText.text += (int)((speedRunTime % 60) - 1);
        speedRunText.text +="."+ ((Math.Round((speedRunTime - (int)(speedRunTime)), 2))*100);
    }
}