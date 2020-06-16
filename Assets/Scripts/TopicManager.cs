using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopicManager : MonoBehaviour
{
    public GameObject[] topics; 

    public float topicSpawnTime;
    private float topicTimeLeft;
    bool timerRunning = true;

    // Start is called before the first frame update
    void Start()
    {
        // Get array of all GameObjects with TopicButton tag to spawn later
        topics = GameObject.FindGameObjectsWithTag("TopicButton");
        Debug.Log(topics[0]);
        topicTimeLeft = topicSpawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        // Timer code
        if(timerRunning){
             topicTimeLeft -= Time.deltaTime;
            if (topicTimeLeft <= 0)
            {
                topicTimeLeft = topicSpawnTime;
                Debug.Log("Spawn Topic");
            }
        }
    }
}
