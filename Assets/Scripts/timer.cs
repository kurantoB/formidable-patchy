﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//quick and messy timer for Formidable Alice mechanics
//probably get rid of logs when timer works 
public class timer : MonoBehaviour
{
    public Text timerText;
    private float maxTime;
    public float timeLeft;

    public Image progressCircle;

    public bool timerRunning = false;

    public delegate void MessageExit();
    private MessageExit msgExit;

    void Update()
    {
        if (timerRunning)
        {
            timeLeft -= Time.deltaTime;
            progressCircle.fillAmount = timeLeft / maxTime;
            if (timeLeft >= 0)
            {
                timerText.text = System.Math.Round(timeLeft, 2).ToString();
            }
            else
            {
                timerText.text = "0.00";
            }


            if (timeLeft <= 0)
            {
                msgExit();
                Debug.Log("timer over");
                timerRunning = false;
            }
        }
    }

    //resets timer, called by Fungus when dialogue has advanced
    public void timerReset(MessageExit msgExit)
    {
        Debug.Log("reset");
        timerRunning = false;
        maxTime = 0f;
        timeLeft = 0f;
        this.msgExit = msgExit;
    }

    //starts timer, called by Fungus when done typing
    public void timerStart(float messageTime)
    {
        maxTime = messageTime;
        timeLeft = messageTime;
        Debug.Log("timer start");
        timerRunning = true;
    }
}
