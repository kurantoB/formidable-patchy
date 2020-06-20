﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopicManager : MonoBehaviour
{
    public static TopicManager instance;
    // Array with all topic GameObjects that can spawn
    public List<GameObject> availableTopics;
    public GameObject[] realTalkTopics;

    public float topicSpawnTime;
    private float topicTimeLeft;

    bool spawningTopics = true;

    // Canvas where topic buttons are drawn
    public GameObject parentCanvas;

    private GameObject spawnedTopic;

    public List<string> visitedTopics;

    public int magicProgress = 0, youkaiProgress = 0;

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
        topicTimeLeft = topicSpawnTime;
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
                spawnedTopic = Instantiate(availableTopics[UnityEngine.Random.Range(0, availableTopics.Count)], new Vector3 (200,200,0), Quaternion.identity, parentCanvas.transform);
                spawnedTopic.SetActive(true);
                // Reset timer
                topicTimeLeft = topicSpawnTime;
            }
        }
    }

    // Keeps track of all topics player has visited
    public void Visited(string topicName){
        
    }

    // Checks if topic has been visited before by comparing topics with list of visited topics
    public bool IsAlreadyVisited(string topic)
    {
            foreach(string topicItem in visitedTopics){
                if(topicItem == topic){
                    // return if topic has been visited
                    Debug.Log(topic + " is an old topic");
                    return true;
                }
            }
            // return if topic has not been visited
            Debug.Log(topic + " is a new topic");
            visitedTopics.Add(topic);
            CheckProgress(topic);
            return false;
    }

    // Clear the scrolling topic list
    public void ClearTopicRoll()
    {
        Debug.Log("clear topics");

        foreach (GameObject topic in GameObject.FindGameObjectsWithTag("TopicButton")){
            Destroy(topic);
        }
        spawningTopics = false;
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

    internal int GetProgress(string newTopic)
    {
        throw new NotImplementedException();
    }

    public void CheckProgress(string topic){
        switch(topic){
            case "Subterranean Animism":
                Debug.Log("Progress made");
                youkaiProgress++;
                break;
            case "Flandre":
                Debug.Log("Progress made");
                youkaiProgress++;
                break;
            case "Marisa's Grimoire":
                Debug.Log("Progress made");
                magicProgress++;
                break;
            case "Patchouli's Health":
                Debug.Log("Progress made");
                magicProgress++;
                break;
            default:
                Debug.Log("No progress made");
            break;
        }

        if(youkaiProgress == 2){
            Debug.Log("Player has visited SA + Flandre, enabling Youkai (Real Talk");
            // Assumes index of casual topic and removes it from spawning, 
            availableTopics.RemoveAt(0);
            // Assumes index of real topic and adds it to spawning list
            availableTopics.Add(realTalkTopics[0]);

            /*
            THERE'S GOTTA BE A BETTER WAY THAN JUST ASSUMING THE INDEX
            LOOK INTO REMOVING AND ADDING GAMEOBJECTS FROM A LIST BY NAME
            It works at the moment as long as the index of the topics aren't switched
            */
        }
        if(magicProgress == 2){
            Debug.Log("Player has visited Grimoire + Health, enabling Magic (Real Talk");
            availableTopics.RemoveAt(1);
            availableTopics.Add(realTalkTopics[1]);
        }       
    }
}
