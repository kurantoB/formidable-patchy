using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
                timerRunning = false;
            }
        }
    }

    public void timerReset(MessageExit msgExit)
    {
        timerRunning = false;
        maxTime = 0f;
        timeLeft = 0f;
        this.msgExit = msgExit;
    }

    public void timerStart(float messageTime)
    {
        maxTime = messageTime;
        timeLeft = messageTime;
        timerRunning = true;
    }
}
