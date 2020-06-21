using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopicManager : MonoBehaviour
{
    public static TopicManager instance;
    // Array with all topic GameObjects that can spawn
    // Uhh I was too lazy to prevent spawning of Real talk topics, so I just made a separate array for topics Patchouli can say
    public List<GameObject> availableTopics, patchyTopics, casualTopics;
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

    public GameFlow gameFlow;

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
                GameObject tentativeTopic;
                while ((tentativeTopic = availableTopics[UnityEngine.Random.Range(0, availableTopics.Count)]).GetComponent<TopicButton>().goToTopic.Equals(gameFlow.GetFlowchartCurrentBlock()));
                spawnedTopic = Instantiate(tentativeTopic, new Vector3 (200,200,0), Quaternion.identity, parentCanvas.transform);
                spawnedTopic.SetActive(true);
                // Reset timer
                topicTimeLeft = topicSpawnTime;
            }
        }
    }

    // Keeps track of all topics player has visited
    public void Visited(string topicName){
        visitedTopics.Add(topicName);
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
            return false;
    }

    // Clear the scrolling topic list
    public void ClearTopicRoll()
    {
        foreach (GameObject topic in GameObject.FindGameObjectsWithTag("TopicButton")){
            Destroy(topic);
        }
        spawningTopics = false;
    }

    // Activate the scrolling topic list
    public void ActivateTopicRoll()
    {
        spawningTopics = true;
    }

    public string GetNextTopic()
    {
        string nextTopic = patchyTopics[UnityEngine.Random.Range(0, 7)].name;
        bool isOldTopic = true;

            if(IsAlreadyVisited(nextTopic)){
                    Debug.Log(nextTopic + " was picked, old topic");
                    nextTopic = GetNextTopic();
            }else{
                if(nextTopic == "Magic Real" || nextTopic == "Becoming a Youkai Real"){
                    Debug.Log(nextTopic + " was picked, but Patchy can't choose it");
                    nextTopic = GetNextTopic();
                }
                    isOldTopic = false;
            }
        // topic initiated by Patchy - can't be already visited, can't be real-talk
        return nextTopic;
    }

    internal int GetProgress(string newTopic)
    {
        switch(newTopic){
            case "Subterranean Animism":
                Debug.Log("Progress made");
                youkaiProgress++;
                if(youkaiProgress == 2){
                    if(!availableTopics.Contains(realTalkTopics[0])){
                        Debug.Log("Player has visited SA + Flandre, enabling Youkai (Real Talk");
                        // Assumes index of casual topic and removes it from spawning, 
                        availableTopics.Remove(casualTopics[0]);
                        // Assumes index of real topic and adds it to spawning list
                        availableTopics.Add(realTalkTopics[0]);
                        
                        
                        /*
                        THERE'S GOTTA BE A BETTER WAY THAN JUST ASSUMING THE INDEX
                        LOOK INTO REMOVING AND ADDING GAMEOBJECTS FROM A LIST BY NAME
                        It works at the moment as long as the index of the topics aren't switched
                        */
                    }

                }
                break;
            case "Flandre":
                Debug.Log("Progress made");
                youkaiProgress++;
                if(youkaiProgress == 2){
                    if(!availableTopics.Contains(realTalkTopics[0])){
                        Debug.Log("Player has visited SA + Flandre, enabling Youkai (Real Talk");
                        // Assumes index of casual topic and removes it from spawning, 
                        availableTopics.Remove(casualTopics[0]);
                        // Assumes index of real topic and adds it to spawning list
                        availableTopics.Add(realTalkTopics[0]);
                        
                        
                        /*
                        THERE'S GOTTA BE A BETTER WAY THAN JUST ASSUMING THE INDEX
                        LOOK INTO REMOVING AND ADDING GAMEOBJECTS FROM A LIST BY NAME
                        It works at the moment as long as the index of the topics aren't switched
                        */
                    }

                }
                break;
            case "Marisa's Grimoire":
                Debug.Log("Progress made");
                magicProgress++;
                if(magicProgress == 2){
                    if(!availableTopics.Contains(realTalkTopics[1])){
                        Debug.Log("Player has visited Grimoire + Health, enabling Magic (Real Talk");
                        availableTopics.Remove(casualTopics[1]);
                        availableTopics.Add(realTalkTopics[1]);
                    }
                }  
                break;
            case "Patchouli's Health":
                Debug.Log("Progress made");
                magicProgress++;
                if(magicProgress == 2){
                    if(!availableTopics.Contains(realTalkTopics[1])){
                        Debug.Log("Player has visited Grimoire + Health, enabling Magic (Real Talk");
                        availableTopics.Remove(casualTopics[1]);
                        availableTopics.Add(realTalkTopics[1]);
                    }
                }  
                break;
            case "Becoming a Youkai (Real Talk)":
                Debug.Log("Progress made");
                youkaiProgress++;
                break;
            case "Magic (Real Talk)":
                Debug.Log("Progress made");
                magicProgress++;
                break;
            default:
                Debug.Log("No progress made");
            break;
        }

        if(youkaiProgress > magicProgress){
            return youkaiProgress;
        }else{
            return magicProgress;
        }
        // if(youkaiProgress > magicProgress){          
        //         if(youkaiProgress == 2){
        //             if(!availableTopics.Contains(realTalkTopics[0])){
        //                 Debug.Log("Player has visited SA + Flandre, enabling Youkai (Real Talk");
        //                 // Assumes index of casual topic and removes it from spawning, 
        //                 availableTopics.RemoveAt(0);
        //                 // Assumes index of real topic and adds it to spawning list
        //                 availableTopics.Add(realTalkTopics[0]);
                        
                        
        //                 /*
        //                 THERE'S GOTTA BE A BETTER WAY THAN JUST ASSUMING THE INDEX
        //                 LOOK INTO REMOVING AND ADDING GAMEOBJECTS FROM A LIST BY NAME
        //                 It works at the moment as long as the index of the topics aren't switched
        //                 */
        //             }

        //         }
        //     return youkaiProgress;
        // }else{
        //     if(magicProgress == 2){
        //         if(!availableTopics.Contains(realTalkTopics[1])){
        //             Debug.Log("Player has visited Grimoire + Health, enabling Magic (Real Talk");
        //             availableTopics.RemoveAt(1);
        //             availableTopics.Add(realTalkTopics[1]);
        //         }
        //     }  
        //     return magicProgress;
        // }
    }

    
}
