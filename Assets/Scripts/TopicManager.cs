using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopicManager : MonoBehaviour
{
    public static TopicManager instance;
    // Array with all topic GameObjects
    public GameObject[] topics;

    public float topicSpawnTime;
    private float topicTimeLeft;

    // Can use to stop timer, will probably need later 
    bool timerRunning = true;

    // Canvas where topic buttons are drawn
    public GameObject parentCanvas;

    private GameObject spawnedTopic;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            // A unique case where the Singleton exists but not in this scene
            if (instance.gameObject.scene.name == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }
        }
    }

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
                spawnedTopic = Instantiate(topics[UnityEngine.Random.Range(0, topics.Length)], new Vector3 (200,200,0), Quaternion.identity, parentCanvas.transform);
                spawnedTopic.SetActive(true);
                // Reset timer
                topicTimeLeft = topicSpawnTime;
            }
        }
    }

    // Receives most recent clicked topic
    public void Visited(string topicName){
        Debug.Log(topicName);

        //make switch statement, enable spawning of real talk version of topics
    }

    public bool IsAlreadyVisited(string topic)
    {
        // return whether this topic is already visited
        return false;
    }

    public void ClearTopicRoll()
    {
        // clear the scrolling topic list
    }

    public void ActivateTopicRoll()
    {
        // activate the scrolling topic list
    }

    public string GetNextTopic()
    {
        // topic initiated by Patchy - can't be already visited, can't be real-talk
        return "Becoming a Youkai"; // placeholder
    }
}
