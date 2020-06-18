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
            flowChart.ExecuteBlock(nextBlock);
            flowChart.SetStringVariable("NextBlock", "Empty");

            // use topic manager to check if this topic was visited already or the current topic is not winding down yet
            // if so, set the NextBlock variable accordingly to Patchy's reaction and disable the scrolling topic list - reenable it when Patchy introduces a new topic

            // otherwise, reenable the scrolling topic list
        }
        else
        {
            bc();
        }
    }

    public void PatchyTopicChange()
    {
        string nextBlock = "Becoming a Youkai";
        flowChart.SetBooleanVariable("PatchyInitiated", true);
        flowChart.ExecuteBlock(nextBlock);
        flowChart.SetStringVariable("NextBlock", "Empty");
    }
}
