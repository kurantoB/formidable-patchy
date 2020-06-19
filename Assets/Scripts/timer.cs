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

    public delegate void MessageExit();
    private MessageExit msgExit;

    void Update()
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
            Debug.Log("Invoking msgExit: " + msgExit);
            msgExit();
        }
}

    public void timerReset(float messageTime, MessageExit msgExit)
    {
        Debug.Log("timerReset");
        this.msgExit = msgExit;
        maxTime = messageTime;
        timeLeft = messageTime;
    }
}
