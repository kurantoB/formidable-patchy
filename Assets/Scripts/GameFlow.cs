using UnityEngine;
using Fungus;

public class GameFlow : MonoBehaviour
{
    public Flowchart flowChart;
    public delegate void BaseContinue();

    public void ChangeTopic(string topic)
    {
        // Temporary logic to remove button
        foreach (GameObject topicButton in GameObject.FindGameObjectsWithTag("TopicButton"))
        {
            GameObject.Destroy(topicButton);
        }

        // clear the topic scrolling list

        flowChart.SetStringVariable("NextBlock", topic);
        flowChart.SetBooleanVariable("PatchyInitiated", false);
    }

    public void HandleContinue(Say sayCommand, BaseContinue bc)
    {
        string nextBlock = flowChart.GetStringVariable("NextBlock");
        if (!"Empty".Equals(nextBlock))
        {
            sayCommand.StopParentBlock();

            // stop topic scrolling

            flowChart.ExecuteBlock(nextBlock);
            if (false) // topic already visited
            {
                flowChart.SetIntegerVariable("ChancesLeft", Mathf.Max(0, flowChart.GetIntegerVariable("ChancesLeft") - 1));
                flowChart.SetStringVariable("NextBlock", "Empty");
                flowChart.SetStringVariable("BadTransition", "Already Talked About");
            }
            else if (!flowChart.GetBooleanVariable("TopicWinddown"))
            {
                flowChart.SetIntegerVariable("ChancesLeft", Mathf.Max(0, flowChart.GetIntegerVariable("ChancesLeft") - 1));
                flowChart.SetBooleanVariable("TopicWinddown", false);
                flowChart.SetStringVariable("CurrentBlock", nextBlock);
                flowChart.SetStringVariable("BadTransition", "Abrupt Topic Change");
                flowChart.SetStringVariable("NextBlock", "Empty");
            } else
            {
                // good transition
                flowChart.SetIntegerVariable("ChancesLeft", Mathf.Max(0, flowChart.GetIntegerVariable("ChancesLeft") - 1));
                flowChart.SetBooleanVariable("TopicWinddown", false);
                flowChart.SetStringVariable("CurrentBlock", nextBlock);
                flowChart.SetStringVariable("NextBlock", "Empty");
                // start topic scrolling
            }
            /*sayCommand.StopParentBlock();
            flowChart.ExecuteBlock(nextBlock);
            flowChart.SetStringVariable("NextBlock", "Empty");*/
        } else {
            bc();
        }
    }

    public void PatchyTopicChange()
    {
        // stop topic scrolling
        
        if (flowChart.GetIntegerVariable("ChancesLeft") == 0)
        {
            Debug.Log("PatchyTopicChange: End of Convo");
            flowChart.FindBlock(flowChart.GetStringVariable("CurrentBlock")).Stop();
            flowChart.ExecuteBlock("End of Convo");
            flowChart.SetStringVariable("NextBlock", "Empty");
            return;
        }

        // get next topic from topic manager
        string nextBlock = "Becoming a Youkai";

        // start topic scrolling

        Debug.Log("PatchyTopicChange: " + nextBlock);
        flowChart.SetIntegerVariable("ChancesLeft", Mathf.Max(0, flowChart.GetIntegerVariable("ChancesLeft") - 1));
        flowChart.SetBooleanVariable("PatchyInitiated", true);
        flowChart.ExecuteBlock(nextBlock);
        flowChart.SetStringVariable("CurrentBlock", nextBlock);
        flowChart.SetStringVariable("NextBlock", "Empty");
        flowChart.SetBooleanVariable("TopicWinddown", false);
    }

    public void ResumeTopic()
    {
        flowChart.ExecuteBlock(flowChart.GetStringVariable("CurrentBlock"));
        flowChart.FindBlock(flowChart.GetStringVariable("CurrentBlock")).JumpToCommandIndex = 2;
        // start topic scrolling
    }
}
