using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopicManager : MonoBehaviour
{
    // Array with all topic GameObjects
    public GameObject[] topics;

    public float topicSpawnTime;
    private float topicTimeLeft;

    // Can use to stop timer, will probably need later 
    bool timerRunning = true;
    
    // Canvas where topic buttons are drawn
    public GameObject parentCanvas;

    private GameObject spawnedTopic;

    // Start is called before the first frame update
    void Start()
    {
        // Get array of all GameObjects with TopicButton tag to spawn later
        //topics = GameObject.FindGameObjectsWithTag("TopicButton");
        topicTimeLeft = topicSpawnTime;

        // Set every original topic button false so they won't move up and delete themselves
        foreach (GameObject topic in topics){
            topic.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Timer code
        if(timerRunning){
             topicTimeLeft -= Time.deltaTime;
            if (topicTimeLeft <= 0)
            {
                // Instantiate random GameObjects in array, set parent to canvas so it'll render
                spawnedTopic = Instantiate(topics[Random.Range(0, topics.Length)], new Vector3 (200,200,0), Quaternion.identity, parentCanvas.transform);
                spawnedTopic.SetActive(true);
                // Reset timer
                topicTimeLeft = topicSpawnTime;
                
            }
        }
    }
}
