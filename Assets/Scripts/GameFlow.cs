using UnityEngine;
using Fungus;

public class GameFlow : MonoBehaviour
{
    public Flowchart flowChart;
    public delegate void BaseContinue();

    public void ChangeTopic(string topic)
    {
        TopicManager.instance.ClearTopicRoll();

        flowChart.SetStringVariable("NextBlock", topic);
        flowChart.SetBooleanVariable("PatchyInitiated", false);
    }

    public void HandleContinue(Say sayCommand, BaseContinue bc)
    {
        string nextBlock = flowChart.GetStringVariable("NextBlock");
        if (!"Empty".Equals(nextBlock))
        {
            sayCommand.StopParentBlock();

            TopicManager.instance.ClearTopicRoll();

            flowChart.ExecuteBlock(nextBlock);
            if (TopicManager.instance.IsAlreadyVisited(nextBlock))
            {
                flowChart.SetIntegerVariable("ChancesLeft", Mathf.Max(0, flowChart.GetIntegerVariable("ChancesLeft") - 1));
                flowChart.SetStringVariable("CurrentBlock", nextBlock);
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

                TopicManager.instance.ActivateTopicRoll();
            }
        } else {
            bc();
        }
    }

    public void PatchyTopicChange()
    {
        TopicManager.instance.ClearTopicRoll();

        if (flowChart.GetIntegerVariable("ChancesLeft") == 0)
        {
            Debug.Log("PatchyTopicChange: End of Convo");
            flowChart.FindBlock(flowChart.GetStringVariable("CurrentBlock")).Stop();
            flowChart.ExecuteBlock("End of Convo");
            flowChart.SetStringVariable("NextBlock", "Empty");
            return;
        }

        string nextBlock = TopicManager.instance.GetNextTopic();

        TopicManager.instance.ActivateTopicRoll();

        Debug.Log("PatchyTopicChange: " + nextBlock);
        flowChart.SetIntegerVariable("ChancesLeft", Mathf.Max(0, flowChart.GetIntegerVariable("ChancesLeft") - 1));
        flowChart.SetBooleanVariable("PatchyInitiated", true);
        flowChart.ExecuteBlock(nextBlock);
        flowChart.SetStringVariable("CurrentBlock", nextBlock);
        flowChart.SetStringVariable("NextBlock", "Empty");
        flowChart.SetBooleanVariable("TopicWinddown", false);
    }

    public void TerminateCurrentTopic()
    {
        flowChart.FindBlock(flowChart.GetStringVariable("CurrentBlock")).Stop();
    }

    public void ResumeTopic()
    {
        TopicManager.instance.ActivateTopicRoll();
    }
}
