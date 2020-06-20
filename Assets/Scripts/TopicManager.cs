using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopicManager : MonoBehaviour
{
    public static TopicManager instance;
    // Array with all topic GameObjects that can spawn
    public GameObject[] topics;

    public float topicSpawnTime;
    private float topicTimeLeft;

    bool spawningTopics = true;

    // Canvas where topic buttons are drawn
    public GameObject parentCanvas;

    private GameObject spawnedTopic;

    public List<string> visitedTopics;

    private bool isSame = false;
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
        if(spawningTopics){
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

    // Keeps track of all topics player has visited
    public void Visited(string topicName){
        // First topic will always be a new topic, add to visited list by default
        if(visitedTopics.Count == 0){
            visitedTopics.Add(topicName);
        }else{
            // Check if topic has been visited before, prevent old topics from being added twice
            if(!IsAlreadyVisited(topicName)){
                visitedTopics.Add(topicName);
                Debug.Log("new topic");
            }else{
                Debug.Log("old topic");
            }

        }
    }

    // Checks if topic has been visited before by comparing topics with list of visited topics
    public bool IsAlreadyVisited(string topic)
    {
        foreach(string topicItem in visitedTopics){
            if(topicItem == topic){
                return true;
            }
        }
        // return whether this topic is already visited
        return false;
    }

    // Clear the scrolling topic list
    public void ClearTopicRoll()
    {
        Debug.Log("clear topics");

        foreach (GameObject topic in GameObject.FindGameObjectsWithTag("TopicButton")){
            Destroy(topic);
        }
    }

    // Activate the scrolling topic list
    public void ActivateTopicRoll()
    {
        
        Debug.Log("activate topic roll");
        spawningTopics = true;
        
    }

    public string GetNextTopic()
    {
        // topic initiated by Patchy - can't be already visited, can't be real-talk
        return "Becoming a Youkai"; // placeholder
    }
}
