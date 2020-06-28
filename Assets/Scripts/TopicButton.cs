using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopicButton : MonoBehaviour

{
    // Upon spawning, button will move upwards until it hits a certain height, where it will delete itself
    private RectTransform  rectTransform;
    private float yPos = -95;
    public int speed;
    public string goToTopic;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        yPos += Time.deltaTime * speed;
        rectTransform.anchoredPosition = new Vector2 (125,yPos);

        if(yPos >= 70){
            Destroy(gameObject);
        }
    }

    public void topicButtonClicked() {
        if (TopicManager.instance.youkaiProgress == 2
            && goToTopic.Equals("Becoming a Youkai"))
        {
            GameObject.FindGameObjectWithTag("GameFlow").GetComponent<GameFlow>().ChangeTopic("Becoming a Youkai (Real Talk)");
        } else if (TopicManager.instance.magicProgress == 2
            && goToTopic.Equals("Magic"))
        {
            GameObject.FindGameObjectWithTag("GameFlow").GetComponent<GameFlow>().ChangeTopic("Magic (Real Talk)");
        } else
        {
            GameObject.FindGameObjectWithTag("GameFlow").GetComponent<GameFlow>().ChangeTopic(goToTopic);
        }

        JSAM.AudioManager.PlaySound(JSAM.Sounds.topicchange);
    }
}
