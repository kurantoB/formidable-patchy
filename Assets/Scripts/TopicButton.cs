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
        GameObject.FindGameObjectWithTag("GameFlow").GetComponent<GameFlow>().ChangeTopic(goToTopic);
    }
}
