using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//quick and messy timer for Formidable Alice mechanics
//probably get rid of logs when timer works 
public class timer : MonoBehaviour
{
    public Text timerText;
    public float timeLeft = 5f;

    public Image progressCircle;

    public bool timerRunning = false;

    void Update()
    {
        if(timerRunning){
            timeLeft -= Time.deltaTime;
            progressCircle.fillAmount = timeLeft/5;
            if(timeLeft >=0){
                timerText.text = System.Math.Round(timeLeft, 2).ToString();
            }else{
                timerText.text = "0.00";
            }
        
            
            if(timeLeft <= 0){
                Debug.Log("timer over");
            }
        }
    }

    //resets timer, called by Fungus when dialogue has advanced
    public void timerReset(){
        timeLeft = 5f;
        Debug.Log("reset");   
        timerRunning = false; 
    }

    //starts timer, called by Fungus when done typing
    public void timerStart(){
        Debug.Log("timer start");
        timerRunning = true;
    }
}
