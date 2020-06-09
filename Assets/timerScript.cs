using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;

public class timerScript : MonoBehaviour
{
    public Text timerText;
    public float timeLeft = 5f;

    public Image progressCircle;

    public bool timerRunning = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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

    public void timerReset(){
        timeLeft = 5f;
        Debug.Log("reset");   
        timerRunning = false; 
    }

    public void timerStart(){
        Debug.Log("timer start");
        timerRunning = true;
    }
}
